using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;
using Store.Presentation.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService, ILogger<AccountController> logger) : base(logger)
        {
            _accountService = accountService;
        }

        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var response = new
            {
                access_token = new JwtHelper().CreateToken(identity),
                username = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string mail, string password)
        {
            var person = _accountService.GetUserModelAsync(mail, password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    //new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
