using System.IdentityModel.Tokens.Jwt;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IJwtProvider
    {
        public string CreateToken(string email, string role);
        public JwtSecurityToken GetPrincipalFromExpiredToken(string token);
        public string GenerateRefreshToken();
    }
}
