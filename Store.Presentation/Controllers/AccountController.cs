using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Services;
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
        public AccountController(IAccountService accountService, ILogger<AccountController> logger) : base(logger)
        {
            _accountService = accountService;
        }

        //[HttpPost]
        //public async Task<IActionResult> RefreshAsync(string token, string refreshToken)
        //{
        //    var principal = new JwtProvider().GetPrincipalFromExpiredToken(token);
        //    var username = principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
        //    var savedRefreshToken = await _accountService.GetRefreshToken(username.Value, username.Issuer, token);
        //    if (savedRefreshToken != refreshToken)
        //        throw new SecurityTokenException("Invalid refresh token");

        //    var newJwtToken = new JwtProvider().CreateToken(principal.Claims);
        //    var newRefreshToken = new JwtProvider().GenerateRefreshToken();
            
        //    //todo save refresh token in db

        //    return new ObjectResult(new
        //    {
        //        token = newJwtToken,
        //        refreshToken = newRefreshToken
        //    });
        //}

        [HttpPost("/token")]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            var identity = await _accountService.GetIdentity(username, password);


            var response = new
            {
                access_token = new JwtProvider().CreateToken(identity.Claims),
                username = identity.Name
            };

            return Ok(response);
        }


    }
}
