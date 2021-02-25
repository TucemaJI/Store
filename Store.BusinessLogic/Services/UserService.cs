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

        public async Task<UserModel> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _userMapper.Map(user);
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var userModelList = new List<UserModel>();

            userModelList = _userMapper.Map(userList);
            return userModelList;
        }
        public async Task<string> GetRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IdentityResult> DeleteUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> UpdateUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user == null)
            {
                user = await _userManager.FindByIdAsync(userModel.Id);
                user.Email = userModel.Email;
            }
            if (!string.IsNullOrWhiteSpace(userModel.Password) && userModel.ConfirmPassword == userModel.Password)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (!result.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionOptions.PASSWORD_NOT_REMOVED);
                }
                var addPassword = await _userManager.AddPasswordAsync(user, userModel.Password);
                if (!addPassword.Succeeded)
                {
                    throw new BusinessLogicException(ExceptionOptions.INCORRECT_PASSWORD);
                }
            }
            if (userModel.FirstName != null)
            {
                user.FirstName = userModel.FirstName;
            }
            if (userModel.LastName != null)
            {
                user.LastName = userModel.LastName;
            }
            if (userModel.IsBlocked != null)
            {
                user.IsBlocked = (bool)userModel.IsBlocked;
            }

            return await _userManager.UpdateAsync(user);
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
                filter.OrderByString = "FirstName";
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
    }
}
