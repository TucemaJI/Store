using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Providers;
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
        private readonly EmailProvider _emailProvider;
        public AccountController(IAccountService accountService, JwtProvider jwtProvider, EmailProvider emailProvider, ILogger<AccountController> logger) : base(logger)
        {
            _accountService = accountService;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshAsync(string token, string refreshToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(token);
            //var test1 = principal.Claims.FirstOrDefault(x=>x.Type.Contains("nameidentifier")).Value;
            var savedRefreshToken = await _accountService.GetRefreshToken(principal);
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newAccessToken = _jwtProvider.CreateToken(principal.Claims);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            await _accountService.WriteRefreshTokenToDb(User, newRefreshToken); // to do everything with this User it is Claim

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

        [HttpPost("Registration")]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return Content("Passwords are different");
            }

            var result = await _accountService.CreateUserAsync(firstName, lastName, email, password);

            if (result.Succeeded)
            {
                var code = await _accountService.SignInAsync(email, password);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = code.Id, code },
                    protocol: HttpContext.Request.Scheme);
                await _emailProvider.SendEmailAsync(email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            }
            else { return Content("Error with creation an account"); }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var result = await _accountService.ConfirmEmailAsync(email, code);
            if (result.Succeeded) { return Content("Email Confirmed"); }
            return Content("Email NOT Confirmed");
        }
    }
}
