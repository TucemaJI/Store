using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserMapper _userMapper;
        public AccountService(UserManager<User> userManager, UserMapper userMapper)
        {
            _userManager = userManager;
            _userMapper = userMapper;
        }

        public async Task<User> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new InvalidOperationException();
            }
            return user;
        }

        public UserModel GetUserModel(User user)
        {
            return _userMapper.Map(user);
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        public async Task<IdentityResult> WriteRefreshTokenToDb(User user, string issuer, string refreshToken)
        {
            return await _userManager.SetAuthenticationTokenAsync(user, issuer, "RefreshToken", refreshToken);
        }

        public async Task<IdentityResult> WriteRefreshTokenToDb(JwtSecurityToken claims, string refreshToken)
        {
            var sub = claims.Subject;
            var user = await _userManager.FindByEmailAsync(sub);
            return await _userManager.SetAuthenticationTokenAsync(user, claims.Issuer, "RefreshToken", refreshToken);
        }

        public async Task<string> GetRefreshToken(JwtSecurityToken claims)
        {
            var sub = claims.Subject;
            var user = await _userManager.FindByEmailAsync(sub);
            return await _userManager.GetAuthenticationTokenAsync(user, claims.Issuer, "RefreshToken");
        }

        public async Task<IdentityResult> CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            User user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = firstName+lastName };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.ConfirmEmailAsync(user, code);
        }
    }
}
