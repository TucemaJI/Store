using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<TokenModel> RefreshAsync(string token, string refreshToken);
        public Task<TokenModel> SignInAsync(string email, string password);
        public Task<IdRoleModel> GetIdUserRoleAsync(string email);
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
