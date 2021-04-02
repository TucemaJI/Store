﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [Authorize]
        [HttpGet("SignOut")]
        public Task<IdentityResult> SignOutAsync()
        {
            var token = HttpContext.Request.Headers[AccountServiceConsts.ACCESS_TOKEN];
            var result = _accountService.SignOutAsync(token);
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
