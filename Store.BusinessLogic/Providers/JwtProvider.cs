using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Services.Interfaces;
using Store.Shared.Options;
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
        private readonly IOptions<JwtOptions> _options;

        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Key));
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options;
        }

        public string CreateToken(string email, string role, string id)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, id),
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Audience,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(_options.Value.Lifetime)),
                    signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _options.Value.Issuer,
                ValidAudience = _options.Value.Audience,

                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.Sub,
                IssuerSigningKey = SecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(ExceptionConsts.INVALID_TOKEN);
            }

            return jwtSecurityToken;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[_options.Value.RefreshTokenLength];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
