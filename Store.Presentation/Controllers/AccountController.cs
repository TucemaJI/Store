using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public Task<TokenModel> RefreshAsync([FromBody] TokenModel model)
        {
            var result = _accountService.RefreshAsync(model);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public Task<TokenModel> SignInAsync([FromBody] SignInModel model)
        {
            var result = _accountService.SignInAsync(model);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("SignInByGoogle")]
        public Task<TokenModel> SignInByGoogleAsync([FromBody] SignInByGoogleModel model)
        {
            var result = _accountService.SignInByGoogleAsync(model);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IdentityResult> RegisterAsync([FromBody] UserModel model)
        {
            var result = await _accountService.RegistrationAsync(model);
            return result;
        }

        [HttpPost("CheckMail")]
        [AllowAnonymous]
        public Task<IdentityResult> ConfirmEmailAsync([FromBody] ConfirmModel model)
        {
            var result = _accountService.ConfirmEmailAsync(model);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("ForgotPassword")]
        public async Task ForgotPasswordAsync(string email)
        {
            await _accountService.RecoveryPasswordAsync(email);
        }
    }
}
