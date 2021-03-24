﻿using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Services.Interfaces;
using Store.Shared.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Providers
{
    public class JwtProvider : IJwtProvider
    {
        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.KEY));

        public string CreateToken(string email, string role, string id)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, id),
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: JwtOptions.ISSUER,
                    audience: JwtOptions.AUDIENCE,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(JwtOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = JwtOptions.ISSUER,
                ValidAudience = JwtOptions.AUDIENCE,

                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.Sub,
                IssuerSigningKey = SecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(ExceptionOptions.INVALID_TOKEN);
            }

            return jwtSecurityToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[JwtOptions.LENGTH];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
