using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger) : base(logger)
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public Task<TokenModel> RefreshAsync([FromBody] TokenModel model)
        {
            return _accountService.RefreshAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public Task<TokenModel> SignInAsync([FromBody] SignInModel model)
        {
            return _accountService.SignInAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<RegistrationModel> RegisterAsync([FromBody] RegistrationModel model)
        {
            await _accountService.RegistrationAsync(model);

            return model;
        }

        [HttpPost("CheckMail")]
        [AllowAnonymous]
        public Task<IdentityResult> ConfirmEmailAsync([FromBody] ConfirmModel model)
        {
            var userModel = _accountService.ConfirmEmailAsync(model);
            return userModel;

        }

        [Authorize]
        [HttpPost("SignOut")]
        public Task<IdentityResult> SignOutAsync()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            return _accountService.SignOutAsync(user.Value, user.Issuer);
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public Task ForgotPasswordAsync(string email)
        {
            return _accountService.RecoveryPasswordAsync(email);
        }
    }
}
