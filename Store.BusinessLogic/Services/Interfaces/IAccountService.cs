using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService 
    {
        public Task<bool> SignInAsync(string email, string password);
        public UserModel GetUserModel(User user);
        public Task<string> GetUserRoleAsync(string email);
        public Task<IdentityResult> WriteRefreshTokenToDb(string email, string issuer, string refreshToken);
        public Task<string> GetRefreshToken(JwtSecurityToken claims);
        public Task<string> CreateConfirmUserAsync(string firstName, string lastName, string email, string password);
        public Task<IdentityResult> ConfirmEmailAsync(string email, string code);
    }
}
