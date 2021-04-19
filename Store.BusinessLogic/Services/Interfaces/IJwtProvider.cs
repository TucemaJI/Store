using Google.Apis.Auth;
using Store.BusinessLogic.Models.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IJwtProvider
    {
        public string CreateToken(string email, string role, string id);
        public JwtSecurityToken GetPrincipalFromExpiredToken(string token);
        public string GenerateRefreshToken();
        public Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(SignInByGoogleModel externalAuth);
    }
}
