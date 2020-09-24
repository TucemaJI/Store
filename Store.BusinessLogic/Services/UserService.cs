using Microsoft.AspNetCore.Identity;
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

        public override async void CreateEntity(UserModel model)
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
            return new UserMapper().Map(await _userManager.FindByEmailAsync(email));
        }

        public override IEnumerable<UserModel> GetModels()
        {
            var userList = _userManager.Users;
            var userModelList = new List<UserModel>();
            foreach (var user in userList)
            {
                userModelList.Add(new UserMapper().Map(user));
            }
            return userModelList;
        }
    }
}
