using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            var userModel =  new UserMapper().Map(user);
            userModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return userModel;
        }
        public async Task<ClaimsIdentity> GetIdentity(string mail, string password)
        {
            var person = await GetUserModelAsync(mail, password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, person.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

    }
}
