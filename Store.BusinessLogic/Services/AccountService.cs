using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task<UserModel> GetUserModelAsync(string email, string password)
        {

            return new UserMapper().Map(await _userManager.FindByEmailAsync(email));
        }
    }
}
