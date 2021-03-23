using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Extentions;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.Shared.Constants;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserMapper _userMapper;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            UserMapper userMapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userMapper = userMapper;
        }

        public async Task CreateUserAsync(UserModel model)
        {
            User user = _userMapper.Map(model);
            IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            }
        }

        public async Task<UserModel> GetUserAsync(string id)
        {
            var user = await FindUserByIdAsync(id);
            if (user is null)
            {
                throw new BusinessLogicException(ExceptionOptions.WRONG_ID);
            }
            return _userMapper.Map(user);
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userModelList = _userMapper.Map(userList);
            return userModelList;
        }
        public async Task<string> GetRoleAsync(string id)
        {
            var user = await FindUserByIdAsync(id);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return role;
        }
        public Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var result = _roleManager.CreateAsync(new IdentityRole(roleName));
            return result;
        }
        public Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var result = _roleManager.Roles.ToListAsync();
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            var user = await FindUserByIdAsync(userModel.Id);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel)// VALIDATE MODEL IN MODEL (I DONT NEED IT CAUSE THEY CAN BE NULL)
        {
            var user = await FindUserByIdAsync(userModel.Id);

            if (!string.IsNullOrWhiteSpace(userModel.Email))
            {
                user.Email = userModel.Email;
            }

            if (!string.IsNullOrWhiteSpace(userModel.Password) && userModel.ConfirmPassword == userModel.Password)
            {
                var removed = await _userManager.RemovePasswordAsync(user);
                if (!removed.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionOptions.PASSWORD_NOT_REMOVED);
                }
                var addPassword = await _userManager.AddPasswordAsync(user, userModel.Password);
                if (!addPassword.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionOptions.INCORRECT_PASSWORD);
                }
            }
            if (!string.IsNullOrWhiteSpace(userModel.FirstName))
            {
                user.FirstName = userModel.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(userModel.LastName))
            {
                user.LastName = userModel.LastName;
            }
            if (userModel.IsBlocked is not null)
            {
                user.IsBlocked = (bool)userModel.IsBlocked;
            }

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task BlockUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.IsBlocked = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task<PageModel<UserModel>> FilterUsersAsync(UserFilter filter)
        {
            var users = _userManager.Users.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"))
                .Where(u => EF.Functions.Like(u.UserName, $"%{filter.Name}%"))
                .Where(u => u.IsBlocked.Equals(filter.IsBlocked));
            if (string.IsNullOrWhiteSpace(filter.OrderByString))
            {
                filter.OrderByString = UserServiceOptions.DEFAULT_SEARCH_STRING;
            }

            var sortedUsers = await PagedList<User>.ToSortedListAsync(source: users.OrderBy(filter.OrderByString, filter.IsDescending),
                pageNumber: filter.EntityParameters.CurrentPage,
                pageSize: filter.EntityParameters.ItemsPerPage);

            var sortedUserModels = _userMapper.Map(sortedUsers);

            var pagedList = PagedList<UserModel>.ToPagedList(sortedUserModels, users.Count(), pageNumber: filter.EntityParameters.CurrentPage,
                pageSize: filter.EntityParameters.ItemsPerPage);

            var pageModel = new PageModel<UserModel>(pagedList);

            return pageModel;

            throw new BusinessLogicException(ExceptionOptions.FILTRATION_PROBLEM);
        }
        private async Task<User> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                throw new BusinessLogicException(ExceptionOptions.NOT_FOUND_USER);
            }
            return user;
        }

    }
}
