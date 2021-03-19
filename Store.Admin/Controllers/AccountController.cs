using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Store.BusinessLogic.Models.Account;

namespace Store.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger) 
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public Task<TokenModel> RefreshAsync([FromBody] TokenModel model)
        {
            return _accountService.RefreshAsync(model.AccessToken, model.RefreshToken);
        }

        [HttpGet]
        public IActionResult SignInAsync()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInModel model)
        {
            var result = await _accountService.SignInAsync(model.Email, model.Password);
            HttpContext.Response.Cookies.Append("accessKey", result.AccessToken);
            HttpContext.Response.Cookies.Append("refreshKey", result.RefreshToken);
            return View();
        }

        [Authorize]
        [HttpPost("SignOut")]
        public async Task<ActionResult> SignOutAsync()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            var result = await _accountService.SignOutAsync(user.Value, user.Issuer);
            return View(result);
        }

    }
}
