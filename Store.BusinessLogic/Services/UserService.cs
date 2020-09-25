using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async void CreateEntityAsync(UserModel model)
        {
            User user = new UserMapper().Map(model);
            IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            }
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserMapper().Map(user);
        }

        public override async Task<IEnumerable<UserModel>> GetModelsAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var userModelList = new List<UserModel>();
            foreach (var user in userList)
            {
                userModelList.Add(new UserMapper().Map(user));
            }
            return userModelList;
        }
    }
}
