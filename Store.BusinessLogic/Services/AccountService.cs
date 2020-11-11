using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserMapper _userMapper;
        private readonly PasswordProvider _passwordProvider;
        private readonly IJwtProvider _jwtProvider;
        private readonly EmailProvider _emailProvider;
        public AccountService(UserManager<User> userManager, UserMapper userMapper,
            PasswordProvider passwordProvider, IJwtProvider jwtProvider, EmailProvider emailProvider)
        {
            _userManager = userManager;
            _userMapper = userMapper;
            _passwordProvider = passwordProvider;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
        }

        public async Task<object> RefreshAsync(string token, string refreshToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(token);
            var savedRefreshToken = await GetRefreshTokenAsync(principal);
            if (savedRefreshToken != refreshToken)
            {
                throw new BusinessLogicException(ExceptionOptions.INVALID_REFRESH_TOKEN);
            }
            var role = await GetUserRoleAsync(principal.Subject);

            var newRefreshToken = _jwtProvider.GenerateRefreshToken();
            
            var result = await WriteRefreshTokenToDbAsync(principal.Subject, principal.Issuer, newRefreshToken);
            
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.REFRESH_TOKEN_NOT_WRITED_TO_DB);
            }

            return new
            {
                token = _jwtProvider.CreateToken(principal.Subject, role),
                refreshToken = newRefreshToken
            };
        }

        public async Task<object> SignInAsync(string email, string password)
        {
            var user = await FindUserByEmailAsync(email);

            if (user.IsBlocked)
            {
                throw new BusinessLogicException(ExceptionOptions.USER_BLOCKED);
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new BusinessLogicException(ExceptionOptions.INCORRECT_PASSWORD);
            }

            var role = await GetUserRoleAsync(email);

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            var result = await WriteRefreshTokenToDbAsync(email, JwtOptions.ISSUER, refreshToken);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.REFRESH_TOKEN_NOT_WRITED_TO_DB);
            }

            return new
            {
                accessToken = _jwtProvider.CreateToken(email, role),
                refreshToken,
            };
        }




        public async Task<string> GetUserRoleAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return role;
        }

        public async Task<IdentityResult> WriteRefreshTokenToDbAsync(string email, string issuer, string refreshToken)
        {
            var user = await FindUserByEmailAsync(email);
            return await _userManager.SetAuthenticationTokenAsync(user, issuer, AccountServiceOptions.REFRESH_TOKEN, refreshToken);
        }

        public async Task<string> GetRefreshTokenAsync(JwtSecurityToken claims)
        {
            var user = await FindUserByEmailAsync(claims.Subject);
            return await _userManager.GetAuthenticationTokenAsync(user, claims.Issuer, AccountServiceOptions.REFRESH_TOKEN);// todo check result
        }

        public async Task<string> CreateConfirmUserAsync(string firstName, string lastName, string email,
            string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new BusinessLogicException(ExceptionOptions.PASSWORDS_DIFFERENT);
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new BusinessLogicException(ExceptionOptions.FIRST_NAME_PROBLEM);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BusinessLogicException(ExceptionOptions.LAST_NAME_PROBLEM);
            }

            User user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = $"{firstName}{lastName}" };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.USER_NOT_CREATED);
            }
            var createdUser = await FindUserByEmailAsync(email);
            return await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
        }


        public async Task<string> ConfirmEmailAsync(string email, string token)
        {
            var user = await FindUserByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.NOT_CONFIRMED);
            }
            return EmailOptions.EMAIL_CONFIRMED;
        }

        public async Task<IdentityResult> SignOutAsync(string email, string issuer)
        {
            var user = await FindUserByEmailAsync(email);
            return await _userManager.RemoveAuthenticationTokenAsync(user, issuer, AccountServiceOptions.REFRESH_TOKEN);
        }

        public async Task RecoveryPasswordAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var password = _passwordProvider.GeneratePassword(AccountServiceOptions.PASSWORD_LENGTH);

            await _userManager.ResetPasswordAsync(user, resetToken, password);

            await _emailProvider.SendEmailAsync(email, EmailOptions.NEW_PASSWORD,
                $"Here is your new password: {password}");
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
