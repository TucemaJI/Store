using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;
using Store.Presentation.Providers;

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

        [HttpPost]
        public async Task<IActionResult> RefreshAsync(string token, string refreshToken)
        {
            var principal = new JwtProvider().GetPrincipalFromExpiredToken(token);
            var username = principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
            var savedRefreshToken = await _accountService.GetRefreshToken(username.Value, username.Issuer, token);
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = new JwtProvider().CreateToken(principal.Claims);
            var newRefreshToken = new JwtProvider().GenerateRefreshToken();
            
            //todo save refresh token in db

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("/token")]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var response = new
            {
                access_token = new JwtProvider().CreateToken(identity.Claims),
                username = identity.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string mail, string password)
        {
            var person = await _accountService.GetUserModelAsync(mail, password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, person.Email),
                    new Claim("Role", person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
