using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using static Store.Shared.Constants.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly EmailProvider _emailProvider;
        public AccountController(IAccountService accountService,
            EmailProvider emailProvider, ILogger<AccountController> logger) 
            : base(logger)
        {
            _accountService = accountService;
            _emailProvider = emailProvider;
        }

        [HttpPost("RefreshToken")]
        public Task<object> RefreshAsync(string token, string refreshToken)
        {
            return _accountService.RefreshAsync(token, refreshToken);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public Task<object> SignInAsync(string email, string password)
        {
            return  _accountService.SignInAsync(email, password);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<string> RegisterAsync(string firstName, string lastName, string email, string password, string confirmPassword)
        {            
            var token = await _accountService.CreateConfirmUserAsync(firstName, lastName, email, password, confirmPassword);

            var callbackUrl = Url.Action(
                EmailOptions.CONFIRM_EMAIL,
                EmailOptions.ACCOUNT,
                new { email, token },
                protocol: HttpContext.Request.Scheme);
            await _emailProvider.SendEmailAsync(email, EmailOptions.CONFIRM_ACOUNT,
                $"Confirm registration using this link: <a href='{callbackUrl}'>link</a>");

            return EmailOptions.COMPLITING_REGISTRATION;
        }

        [HttpGet("CheckMail")]
        [AllowAnonymous]
        public Task<string> ConfirmEmailAsync(string email, string token)
        {
            return _accountService.ConfirmEmailAsync(email, token);
           
        }

        [Authorize]
        [HttpPost("SignOut")]
        public Task<IdentityResult> SignOutAsync()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            return _accountService.SignOutAsync(user.Value, user.Issuer);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public Task ForgotPasswordAsync(string email)
        {
            return _accountService.RecoveryPasswordAsync(email);

        }
    }
}
