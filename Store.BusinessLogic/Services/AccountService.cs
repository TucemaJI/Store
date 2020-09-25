using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetRefreshToken(string email, string loginProvider, string tokenName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
        }

        public async Task<UserModel> GetUserModelAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserMapper().Map(user);
        }
    }
}
