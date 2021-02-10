using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.Presentation.Models.AccountModels;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<object> RefreshAsync(string token, string refreshToken);
        public Task<TokenModel> SignInAsync(string email, string password);
        public Task<string> GetUserRoleAsync(string email);
        public Task<IdentityResult> WriteRefreshTokenToDbAsync(string email, string issuer,
            string refreshToken);
        public Task<string> GetRefreshTokenAsync(JwtSecurityToken claims);
        public Task<string> CreateConfirmUserAsync(string firstName, string lastName, string email,
            string password, string confirmPassword);
        public Task<UserModel> ConfirmEmailAsync(string email, string token, string password);
        public Task<IdentityResult> SignOutAsync(string email, string issuer);
        public Task RecoveryPasswordAsync(string email);
    }
}
