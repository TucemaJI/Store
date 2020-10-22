using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using Store.Presentation.Providers;
using Store.Shared.Constants;
using static Store.Shared.Constants.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class AccountController : BaseController// todo no logic in controller (create IjwtProveder in blogic)(reg in startup)
    {
        private readonly IAccountService _accountService;
        private readonly JwtProvider _jwtProvider;
        private readonly EmailProvider _emailProvider;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, JwtProvider jwtProvider, EmailProvider emailProvider, ILogger<AccountController> logger, IConfiguration configuration) : base(logger)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
            _configuration = configuration;
        }

        [HttpPost("RefreshToken")]
        public async Task<object> RefreshAsync(string token, string refreshToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(token);
            var savedRefreshToken = await _accountService.GetRefreshTokenAsync(principal);
            if (savedRefreshToken != refreshToken)
            {
                throw new BusinessLogicException(ExceptionOptions.InvalidRefreshToken);
            }
            var role = await _accountService.GetUserRoleAsync(principal.Subject);

            var newRefreshToken = _jwtProvider.GenerateRefreshToken();
            //todo check IdResult to service
            await _accountService.WriteRefreshTokenToDbAsync(principal.Subject, principal.Issuer, newRefreshToken);

            return new
            {
                token = _jwtProvider.CreateToken(principal.Subject, role),
                refreshToken = newRefreshToken
            };
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<object> SignInAsync(string email, string password)
        {
            await _accountService.SignInAsync(email, password);

            var role = await _accountService.GetUserRoleAsync(email);

            var refreshToken = _jwtProvider.GenerateRefreshToken();
            await _accountService.WriteRefreshTokenToDbAsync(email, JwtOptions.Issuer, refreshToken);//todo check IdResult to service

            return new
            {
                accessToken = _jwtProvider.CreateToken(email, role),
                refreshToken,
            };
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<string> RegisterAsync(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new BusinessLogicException(ExceptionOptions.PasswordsAreDifferent);
            }

            // todo check fname lname

            var token = await _accountService.CreateConfirmUserAsync(firstName, lastName, email, password);

            var callbackUrl = Url.Action(
                EmailOptions.ConfirmEmail,
                EmailOptions.Account,
                new { email, token },
                protocol: HttpContext.Request.Scheme);
            await _emailProvider.SendEmailAsync(email, EmailOptions.ConfirmYourAccount,
                $"Confirm registration using this link: <a href='{callbackUrl}'>link</a>");

            return EmailOptions.ToCompleteRegistration;
        }

        [HttpGet("CheckMail")]
        [AllowAnonymous]
        public async Task<string> ConfirmEmailAsync(string email, string token)
        {
            var result = await _accountService.ConfirmEmailAsync(email, token);
            if (result.Succeeded)
            {
                return EmailOptions.EmailConfirmed;
            }
            throw new BusinessLogicException(ExceptionOptions.NotConfirmed);
        }

        [Authorize]
        [HttpPost("SignOut")]
        public async Task<IdentityResult> SignOutAsync()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            return await _accountService.SignOutAsync(user.Value, user.Issuer);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task ForgotPasswordAsync(string email)
        {
            var password = await _accountService.RecoveryPasswordAsync(email);

            await _emailProvider.SendEmailAsync(email, EmailOptions.YourNewPassword,
                $"Here is your new password: {password}");
        }
    }
}
