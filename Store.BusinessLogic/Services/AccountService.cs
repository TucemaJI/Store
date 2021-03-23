using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.Shared.Constants;
using Store.Shared.Enums;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RegisterMapper _registerMapper;
        private readonly PasswordProvider _passwordProvider;
        private readonly IJwtProvider _jwtProvider;
        private readonly EmailProvider _emailProvider;
        public AccountService(UserManager<User> userManager, RegisterMapper registerMapper,
            PasswordProvider passwordProvider, IJwtProvider jwtProvider, EmailProvider emailProvider)
        {
            _userManager = userManager;
            _registerMapper = registerMapper;
            _passwordProvider = passwordProvider;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
        }

        public async Task<TokenModel> RefreshAsync(TokenModel model)
        {
            if (model.RefreshToken is null)
            {
                throw new BusinessLogicException(ExceptionOptions.NO_REFRESH_TOKEN);
            }

            var principal = _jwtProvider.GetPrincipalFromExpiredToken(model.AccessToken);

            var user = await FindUserByEmailAsync(principal.Subject);

            var authenticationToken = await _userManager.GetAuthenticationTokenAsync(user, principal.Issuer, AccountServiceOptions.REFRESH_TOKEN);

            if (authenticationToken == string.Empty)
            {
                throw new BusinessLogicException(ExceptionOptions.NO_REFRESH_TOKEN);
            }

            if (authenticationToken != model.RefreshToken)
            {
                throw new BusinessLogicException(ExceptionOptions.INVALID_REFRESH_TOKEN);
            }

            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            var result = await _userManager.SetAuthenticationTokenAsync(user, principal.Issuer, AccountServiceOptions.REFRESH_TOKEN, newRefreshToken);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.REFRESH_TOKEN_NOT_WRITED_TO_DB);
            }

            var role = principal.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            var id = principal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var returnToken = new TokenModel
            {
                AccessToken = _jwtProvider.CreateToken(principal.Subject, role, id),
                RefreshToken = newRefreshToken
            };
            return returnToken;
        }

        public async Task<TokenModel> SignInAsync(SignInModel model)
        {
            var user = await FindUserByEmailAsync(model.Email);
            if (user is null)
            {
                throw new BusinessLogicException(ExceptionOptions.NOT_FOUND_USER);
            }

            if (user.IsBlocked)
            {
                throw new BusinessLogicException(ExceptionOptions.USER_BLOCKED);
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new BusinessLogicException(ExceptionOptions.INCORRECT_PASSWORD);
            }

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            var result = await _userManager.SetAuthenticationTokenAsync(user, JwtOptions.ISSUER, AccountServiceOptions.REFRESH_TOKEN, refreshToken);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.REFRESH_TOKEN_NOT_WRITED_TO_DB);
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var token = new TokenModel
            {
                AccessToken = _jwtProvider.CreateToken(model.Email, role, user.Id),
                RefreshToken = refreshToken,
            };

            return token;
        }

        public async Task<IdentityResult> RegistrationAsync(RegistrationModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new BusinessLogicException(ExceptionOptions.PASSWORDS_DIFFERENT);
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                throw new BusinessLogicException(ExceptionOptions.FIRST_NAME_PROBLEM);
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new BusinessLogicException(ExceptionOptions.LAST_NAME_PROBLEM);
            }

            var user = _registerMapper.Map(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.USER_NOT_CREATED);
            }
            var role = await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            if (!role.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.NOT_ADD_TO_ROLE);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = string.Format(AccountServiceOptions.CALLBACK_URL, user.Email, user.FirstName, user.LastName, WebUtility.UrlEncode(token));
            await _emailProvider.SendEmailAsync(user.Email, EmailOptions.CONFIRM_ACOUNT, string.Format(AccountServiceOptions.MESSAGE, callbackUrl));
            return role;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ConfirmModel model)
        {
            var user = await FindUserByEmailAsync(model.Email);
            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(ExceptionOptions.NOT_CONFIRMED);
            }
            return result;
        }

        public async Task<IdentityResult> SignOutAsync(string email, string issuer)
        {
            var user = await FindUserByEmailAsync(email);
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, issuer, AccountServiceOptions.REFRESH_TOKEN);
            return result;
        }

        public async Task RecoveryPasswordAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var password = _passwordProvider.GeneratePassword(AccountServiceOptions.PASSWORD_LENGTH);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, password);

            if (result.Succeeded)
            {
                await _emailProvider.SendEmailAsync(email, EmailOptions.NEW_PASSWORD,
                    string.Concat(EmailOptions.NEW_PASSWORD, password));
            }
        }
        private async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new BusinessLogicException($"{ExceptionOptions.NOT_FOUND_USER}{email}");
            }
            return user;
        }
    }
}
