using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
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
        public Task<IdentityResult> WriteRefreshTokenToDb(ClaimsPrincipal claims, string refreshToken);
        public Task<string> GetRefreshToken(ClaimsPrincipal claims);
    }
}
