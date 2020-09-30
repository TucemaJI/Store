using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserMapper _userMapper;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, UserMapper userMapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userMapper = userMapper;
        }

        public override async void CreateEntityAsync(UserModel model)
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

        public override async Task<IEnumerable<UserModel>> GetModelsAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var userModelList = new List<UserModel>();
            foreach (var user in userList)
            {
                userModelList.Add(_userMapper.Map(user));
            }
            return userModelList;
        }
        public async Task<string> GetRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IdentityResult> DeleteUser(UserModel userModel)
        {
            var user = _userMapper.Map(userModel);
            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> UpdateUser(UserModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            return await _userManager.UpdateAsync(user);
        }

    }
}
