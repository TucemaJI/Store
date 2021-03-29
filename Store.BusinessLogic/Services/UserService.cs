using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Extentions;
using Store.DataAccess.Models.Filters;
using Store.Shared.Enums;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserMapper _userMapper;

        public UserService(UserManager<User> userManager, UserMapper userMapper)
        {
            _userManager = userManager;
            _userMapper = userMapper;
        }

        public async Task CreateUserAsync(UserModel model)
        {
            var user = _userMapper.Map(model);
            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                throw new BusinessLogicException(ExceptionConsts.USER_NOT_CREATED);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            if (!roleResult.Succeeded)
            {
                throw new BusinessLogicException(ExceptionConsts.NOT_ADD_TO_ROLE);
            }
        }

        public async Task<UserModel> GetUserAsync(string id)
        {
            var user = await FindUserByIdAsync(id);
            return _userMapper.Map(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await FindUserByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel)//add validation change user in front
        {
            var user = await FindUserByIdAsync(userModel.Id);

            if (!string.IsNullOrWhiteSpace(userModel.Password) || userModel.ConfirmPassword == userModel.Password)//in if throw...
            {
                var removed = await _userManager.RemovePasswordAsync(user);
                if (!removed.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionConsts.PASSWORD_NOT_REMOVED);
                }
                var addPassword = await _userManager.AddPasswordAsync(user, userModel.Password);
                if (!addPassword.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionConsts.INCORRECT_PASSWORD);
                }
            }

            user = _userMapper.Map(userModel);

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task BlockUserAsync(BlockModel model)
        {
            var user = await FindUserByIdAsync(model.Id);
            user.IsBlocked = model.Block;
            await _userManager.UpdateAsync(user);
        }

        public async Task<PageModel<UserModel>> FilterUsersAsync(UserFilter filter)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByString))
            {
                filter.OrderByString = UserServiceConsts.DEFAULT_SEARCH_STRING;
            }
            var users = await _userManager.Users.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"))
                .Where(u => EF.Functions.Like(u.UserName, $"%{filter.Name}%"))
                .Where(u => filter.IsBlocked.Equals(null) || u.IsBlocked.Equals(filter.IsBlocked))
                .OrderBy(filter.OrderByString, filter.IsDescending)
                .ToSortedListAsync(pageNumber: filter.PageOptions.CurrentPage, pageSize: filter.PageOptions.ItemsPerPage);

            var sortedUserModels = _userMapper.Map(users);

            filter.PageOptions.TotalItems = await _userManager.Users.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"))
                .Where(u => EF.Functions.Like(u.UserName, $"%{filter.Name}%"))
                .Where(u => filter.IsBlocked.Equals(null) || u.IsBlocked.Equals(filter.IsBlocked))
                .CountAsync();

            var pageModel = new PageModel<UserModel>(sortedUserModels, filter.PageOptions);

            return pageModel;

            throw new BusinessLogicException(ExceptionConsts.FILTRATION_PROBLEM);
        }
        private async Task<User> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                throw new BusinessLogicException(ExceptionConsts.NOT_FOUND_USER);
            }
            return user;
        }

    }
}
