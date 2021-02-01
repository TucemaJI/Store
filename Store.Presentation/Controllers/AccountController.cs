using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using Store.Presentation.Models.AccountModels;
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
        public Task<object> RefreshAsync([FromBody]TokenModel model)
        {
            return _accountService.RefreshAsync(model.AccessToken, model.RefreshToken);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public Task<object> SignInAsync([FromBody]SignInModel model)
        {
            return  _accountService.SignInAsync(model.Email, model.Password);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<string> RegisterAsync([FromBody]RegistrationModel model)
        {            
            var token = await _accountService.CreateConfirmUserAsync(model.FirstName, model.LastName, model.Email, model.Password, model.ConfirmPassword);

            var callbackUrl = Url.Action(
                EmailOptions.CONFIRM_EMAIL,
                EmailOptions.ACCOUNT,
                new { model.Email, token },
                protocol: HttpContext.Request.Scheme);
            await _emailProvider.SendEmailAsync(model.Email, EmailOptions.CONFIRM_ACOUNT,
                $"Confirm registration using this link: <a href='{callbackUrl}'>link</a>");

            return EmailOptions.COMPLITING_REGISTRATION;
        }

        [HttpGet("CheckMail")]
        [AllowAnonymous]
        public Task<string> ConfirmEmailAsync([FromBody]ConfirmModel model)
        {
            return _accountService.ConfirmEmailAsync(model.Email, model.Token);
           
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
        public Task ForgotPasswordAsync([FromBody]string email)
        {
            return _accountService.RecoveryPasswordAsync(email);

        }
    }
}
