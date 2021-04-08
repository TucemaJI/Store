using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Account;
using Store.BusinessLogic.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

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
            return _accountService.RefreshAsync(model);
        }

        [HttpGet]
        public IActionResult SignInAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInModel model)
        {
            var result = await _accountService.SignInAsync(model);
            HttpContext.Response.Cookies.Append("accessKey", result.AccessToken);
            HttpContext.Response.Cookies.Append("refreshKey", result.RefreshToken);
            return View();
        }

        [Authorize]
        [HttpPost("SignOut")]
        public async Task<ActionResult> SignOutAsync()
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            //var result = await _accountService.SignOutAsync(user.Value, user.Issuer);
            return View();
        }

    }
}
