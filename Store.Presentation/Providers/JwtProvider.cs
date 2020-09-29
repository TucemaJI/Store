using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Store.Presentation.Providers
{
    public class JwtProvider
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1;
        private readonly SymmetricSecurityKey securityKey;
        public JwtProvider()
        {
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return securityKey;
        }
        public ClaimsIdentity GetIdentity(string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(LIFETIME)),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = ISSUER,
                ValidAudience = AUDIENCE,

                ClockSkew = TimeSpan.Zero,

                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
