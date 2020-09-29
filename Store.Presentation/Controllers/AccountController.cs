using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Controllers.Base;
using Store.Presentation.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly JwtProvider _jwtProvider;
        public AccountController(IAccountService accountService, JwtProvider jwtProvider, ILogger<AccountController> logger) : base(logger)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshAsync(string token, string refreshToken)
        {
            var jwtSequrityToken = _jwtProvider.GetPrincipalFromExpiredToken(token);
            //var test1 = principal.Claims.FirstOrDefault(x=>x.Type.Contains("nameidentifier")).Value;
            var savedRefreshToken = await _accountService.GetRefreshToken(jwtSequrityToken);
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newAccessToken = _jwtProvider.CreateToken(jwtSequrityToken.Claims);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            await _accountService.WriteRefreshTokenToDb(jwtSequrityToken, newRefreshToken);

            return new ObjectResult(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _accountService.SignInAsync(email, password);
            var role = await _accountService.GetUserRoleAsync(user);
            var identity = _jwtProvider.GetIdentity(user.Email, role);

            var refreshToken = _jwtProvider.GenerateRefreshToken();
            await _accountService.WriteRefreshTokenToDb(user, JwtProvider.ISSUER, refreshToken);

            var response = new
            {
                accessToken = _jwtProvider.CreateToken(identity.Claims),
                refreshToken,
            };

            return Ok(response);
        }


    }
}
