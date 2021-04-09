using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserMapper _userMapper;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, UserMapper userMapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userMapper = userMapper;
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserModel model)
        {
            var user = _userMapper.Map(model);
            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.USER_NOT_CREATED);
                throw new BusinessLogicException(model.Errors.ToList());
            }
            var roleResult = await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            if (!roleResult.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.NOT_ADD_TO_ROLE);
                throw new BusinessLogicException(model.Errors.ToList());
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
        public async Task<UserModel> UpdateUserAsync(UserModel userModel)
        {
            var user = await FindUserByIdAsync(userModel.Id);

            if (!string.IsNullOrWhiteSpace(userModel.Password) && userModel.ConfirmPassword == userModel.Password)
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, userModel.Password);
            }
            _userMapper.MapExist(userModel, user);

            var result = await _userManager.UpdateAsync(user);
            userModel = _userMapper.Map(user);
            if (!result.Succeeded)
            {
                userModel.Errors.Add(ExceptionConsts.USER_NOT_UPDATE);
                throw new BusinessLogicException(userModel.Errors.ToList());
            }
            return userModel;
        }

        public async Task BlockUserAsync(string id)
        {
            var user = await FindUserByIdAsync(id);
            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);
        }

        public async Task<PageModel<UserModel>> FilterUsersAsync(UserFilter filter)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByField))
            {
                filter.OrderByField = UserServiceConsts.DEFAULT_SEARCH_STRING;
            }
            var users = await _userRepository.FilterUsersAsync(filter);

            var sortedUserModels = _userMapper.Map(users);

            await _userRepository.CountAsync(filter);

            var pageModel = new PageModel<UserModel>(sortedUserModels, filter.PageOptions);

            return pageModel;

        }
        private async Task<User> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.NOT_FOUND_USER });
            }
            return user;
        }

    }
}
