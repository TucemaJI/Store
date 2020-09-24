using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Presentation.Helpers
{
    public class JwtHelper
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        private readonly SymmetricSecurityKey securituKey;
        public JwtHelper()
        {
            securituKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return securituKey;
        }
        public string CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: JwtHelper.ISSUER,
                    audience: JwtHelper.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(JwtHelper.LIFETIME)),
                    signingCredentials: new SigningCredentials(new JwtHelper().GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
