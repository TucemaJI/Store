using Microsoft.IdentityModel.Tokens;
using Store.Shared.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Store.Shared.Constants.Constants;
using Microsoft.Extensions.Configuration;

namespace Store.Presentation.Providers
{
    public class JwtProvider
    {
        public readonly SymmetricSecurityKey securityKey;
        private readonly IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[JwtOptions.Key]));
        }

        public string CreateToken(string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: _configuration[JwtOptions.Issuer],
                    audience: _configuration[JwtOptions.Audience],
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(double.Parse(_configuration[JwtOptions.Lifetime]))),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration[JwtOptions.Issuer],
                ValidAudience = _configuration[JwtOptions.Audience],

                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.Sub,
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(ExceptionOptions.InvalidToken);
            }

            return jwtSecurityToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[byte.Parse(_configuration[JwtOptions.RefreshTokenLength])];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
