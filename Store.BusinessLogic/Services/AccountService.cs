using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.Shared.Enums;
using Store.Shared.Options;
using System.Collections.Generic;
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
        private readonly UserMapper _userMapper;
        private readonly PasswordProvider _passwordProvider;
        private readonly IJwtProvider _jwtProvider;
        private readonly EmailProvider _emailProvider;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly string _callbackUrl;
        public AccountService(UserManager<User> userManager, UserMapper userMapper,
            PasswordProvider passwordProvider, IJwtProvider jwtProvider, EmailProvider emailProvider,
            SignInManager<User> signInManager, IOptions<JwtOptions> jwtOptions, IOptions<ServiceOptions> serviceOptions)
        {
            _userManager = userManager;
            _userMapper = userMapper;
            _passwordProvider = passwordProvider;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
            _signInManager = signInManager;
            _jwtOptions = jwtOptions;
            _callbackUrl = serviceOptions.Value.AccountCallbackUrl;
        }

        public async Task<TokenModel> RefreshAsync(TokenModel model)
        {
            if (model.RefreshToken is null)
            {
                model.Errors.Add(ExceptionConsts.NO_REFRESH_TOKEN);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            var principal = _jwtProvider.GetPrincipalFromExpiredToken(model.AccessToken);

            var user = await FindUserByEmailAsync(principal.Subject);
            if (user is null)
            {
                model.Errors.Add(ExceptionConsts.NOT_FOUND_USER);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            string authenticationToken = await _userManager.GetAuthenticationTokenAsync(user, principal.Issuer, AccountServiceConsts.REFRESH_TOKEN);

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                model.Errors.Add(ExceptionConsts.NO_REFRESH_TOKEN);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            if (authenticationToken != model.RefreshToken)
            {
                model.Errors.Add(ExceptionConsts.INVALID_REFRESH_TOKEN);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            string newRefreshToken = _jwtProvider.GenerateRefreshToken();

            var result = await _userManager.SetAuthenticationTokenAsync(user, principal.Issuer, AccountServiceConsts.REFRESH_TOKEN, newRefreshToken);

            if (!result.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.INVALID_TOKEN);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            await _signInManager.RefreshSignInAsync(user);

            string role = principal.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            string id = principal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

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

            if (user.IsBlocked)
            {
                model.Errors.Add(ExceptionConsts.USER_BLOCKED);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!signIn.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.SIGN_IN_FAILED);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            string refreshToken = _jwtProvider.GenerateRefreshToken();

            var result = await _userManager.SetAuthenticationTokenAsync(user, _jwtOptions.Value.Issuer, AccountServiceConsts.REFRESH_TOKEN, refreshToken);

            if (!result.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.REFRESH_TOKEN_NOT_WRITED);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var token = new TokenModel
            {
                AccessToken = _jwtProvider.CreateToken(model.Email, role, user.Id),
                RefreshToken = refreshToken,
            };

            return token;
        }

        public async Task<IdentityResult> RegistrationAsync(UserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password != model.ConfirmPassword)
            {
                model.Errors.Add(ExceptionConsts.PASSWORDS_DIFFERENT);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            var isExist = await _userManager.FindByEmailAsync(model.Email);
            if (isExist is not null)
            {
                model.Errors.Add(ExceptionConsts.USER_EXIST);
                throw new BusinessLogicException(model.Errors.ToList());
            }

            var user = _userMapper.Map(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.USER_NOT_CREATED);
                throw new BusinessLogicException(model.Errors.ToList());
            }
            var identityResult = await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());
            if (!identityResult.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.NOT_ADD_TO_ROLE);
                throw new BusinessLogicException(model.Errors.ToList());
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string callbackUrl = string.Format(_callbackUrl, user.Email, user.FirstName, user.LastName, WebUtility.UrlEncode(token));
            await _emailProvider.SendEmailAsync(user.Email, EmailConsts.CONFIRM_ACOUNT, string.Format(AccountServiceConsts.MESSAGE, callbackUrl));
            return identityResult;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ConfirmModel model)
        {
            var user = await FindUserByEmailAsync(model.Email);
            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (!result.Succeeded)
            {
                model.Errors.Add(ExceptionConsts.NOT_CONFIRMED);
                throw new BusinessLogicException(model.Errors.ToList());
            }
            return result;
        }

        public async Task<IdentityResult> SignOutAsync(string accessToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);
            if (principal is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.INVALID_TOKEN });
            }
            var user = await FindUserByEmailAsync(principal.Subject);
            if (user is null)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.NOT_FOUND_USER });
            }
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, principal.Issuer, AccountServiceConsts.REFRESH_TOKEN);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(new List<string> { ExceptionConsts.INVALID_REFRESH_TOKEN });
            }
            await _signInManager.SignOutAsync();
            return result;
        }

        public async Task RecoveryPasswordAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            string password = _passwordProvider.GeneratePassword(AccountServiceConsts.PASSWORD_LENGTH);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, password);

            if (result.Succeeded)
            {
                await _emailProvider.SendEmailAsync(email, EmailConsts.NEW_PASSWORD,
                    string.Concat(EmailConsts.NEW_PASSWORD, password));
            }
        }
        private async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new BusinessLogicException(new List<string> { $"{ExceptionConsts.NOT_FOUND_USER}{email}" });
            }
            return user;
        }
    }
}
