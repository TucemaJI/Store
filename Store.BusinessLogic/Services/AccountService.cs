using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.Shared.Constants;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

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

        public async Task SignInAsync(string email, string password)
        {
            var user = await FindUserByEmailAsync(email);

            if (user.IsBlocked)
            {
                throw new BusinessLogicException(ExceptionOptions.UserBlocked);
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new BusinessLogicException(ExceptionOptions.IncorrectPassword);
            }
        }

        public UserModel GetUserModel(User user)
        {
            return _userMapper.Map(user);
        }

        public async Task<string> GetUserRoleAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        public async Task<IdentityResult> WriteRefreshTokenToDbAsync(string email, string issuer, string refreshToken)
        {
            var user = await FindUserByEmailAsync(email);
            return await _userManager.SetAuthenticationTokenAsync(user, issuer, AccountServiceOptions.RefreshToken, refreshToken);
        }

        public async Task<string> GetRefreshTokenAsync(JwtSecurityToken claims)
        {
            var user = await FindUserByEmailAsync(claims.Subject);
            return await _userManager.GetAuthenticationTokenAsync(user, claims.Issuer, AccountServiceOptions.RefreshToken);
        }

        public async Task<string> CreateConfirmUserAsync(string firstName, string lastName, string email, string password)
        {
            User user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = $"{firstName} {lastName}" };
            await _userManager.CreateAsync(user, password);
            var createdUser = await FindUserByEmailAsync(email);
            return await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
        }


        public async Task<IdentityResult> ConfirmEmailAsync(string email, string token)
        {
            var user = await FindUserByEmailAsync(email);
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> SignOutAsync(string email, string issuer)
        {
            var user = await FindUserByEmailAsync(email);
            return await _userManager.RemoveAuthenticationTokenAsync(user, issuer, AccountServiceOptions.RefreshToken);
        }

        public async Task<string> RecoveryPasswordAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var password = string.Empty;
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[9];
                cryptRNG.GetBytes(tokenBuffer);
                password = Convert.ToBase64String(tokenBuffer);
            }

            await _userManager.ResetPasswordAsync(user, resetToken, password);
            return password;
        }
        private async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }
            throw new BusinessLogicException($"Not found {email} user");
        }
    }
}
