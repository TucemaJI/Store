using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService 
    {
        public Task<User> SignInAsync(string email, string password);
        public UserModel GetUserModel(User user);
        public Task<string> GetUserRoleAsync(User user);
        public Task<IdentityResult> WriteRefreshTokenToDb(User user, string issuer, string refreshToken);
        public Task<IdentityResult> WriteRefreshTokenToDb(JwtSecurityToken claims, string refreshToken);
        public Task<string> GetRefreshToken(JwtSecurityToken claims);
        public Task<IdentityResult> CreateUserAsync(string firstName, string lastName, string email, string password);
        public Task<IdentityResult> ConfirmEmailAsync(string email, string code);
    }
}
