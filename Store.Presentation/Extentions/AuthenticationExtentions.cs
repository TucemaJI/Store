using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Extentions
{
    public static class AuthenticationExtentions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddCookie(IdentityConstants.ApplicationScheme)
                    .AddCookie(IdentityConstants.ExternalScheme)
                    .AddCookie(IdentityConstants.TwoFactorUserIdScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = configuration[JwtConsts.ISSUER],
                            ValidAudience = configuration[JwtConsts.AUDIENCE],
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,

                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[JwtConsts.KEY])),
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }
    }
}
