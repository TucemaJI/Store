using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Presentation.Providers;
using System;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Extentions
{
    public static class AuthenticationExtentions
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = JwtOptions.ISSUER,
                            ValidAudience = JwtOptions.AUDIENCE,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,

                            IssuerSigningKey = new JwtProvider().securityKey,
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }
    }
}
