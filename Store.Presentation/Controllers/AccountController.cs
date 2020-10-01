using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            var savedRefreshToken = await _accountService.GetRefreshTokenAsync(principal);
            if (savedRefreshToken != refreshToken) 
            { 
                throw new SecurityTokenException("Invalid refresh token"); 
            }
            var role = await _accountService.GetUserRoleAsync(principal.Subject);
            var newAccessToken = _jwtProvider.CreateToken(principal.Subject, role);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            await _accountService.WriteRefreshTokenToDbAsync(principal.Subject, principal.Issuer, newRefreshToken);

            return new ObjectResult(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            var result = await _accountService.SignInAsync(email, password);
            if (!result) 
            { 
                throw new Exception(); 
            }
            var role = await _accountService.GetUserRoleAsync(email);

            var refreshToken = _jwtProvider.GenerateRefreshToken();
            await _accountService.WriteRefreshTokenToDbAsync(email, _jwtProvider.GetIssuer(), refreshToken);

            var response = new
            {
                accessToken = _jwtProvider.CreateToken(email, role),
                refreshToken,
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterAsync(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return Content("Passwords are different");
            }

            var token = await _accountService.CreateConfirmUserAsync(firstName, lastName, email, password);

            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { email, token },
                protocol: HttpContext.Request.Scheme);
            await _emailProvider.SendEmailAsync(email, "Confirm your account",
                $"Confirm registration using this link: <a href='{callbackUrl}'>link</a>");

            return Content("To complete the registration, check your email and follow the link provided in the letter");
        }

        [HttpGet("CheckMail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string token)
        {
            var result = await _accountService.ConfirmEmailAsync(email, token);
            if (result.Succeeded) 
            {
                return Content("Email Confirmed"); 
            }
            return Content("Email NOT Confirmed");
        }

        [Authorize]
        [HttpPost("SignOut")]
        public async Task<IdentityResult> SignOutAsync() 
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.Sub);
            return await _accountService.SignOutAsync(user.Value, user.Issuer);
        }
    }
}
