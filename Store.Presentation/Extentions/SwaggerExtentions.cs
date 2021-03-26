using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Linq;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Extentions
{
    public static class SwaggerExtentions
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(configuration[StartupConsts.VERSION], new OpenApiInfo
                {
                    Version = configuration[StartupConsts.VERSION],
                    Title = configuration[StartupConsts.TITLE_APPLICATION]
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition(configuration[StartupConsts.BEARER],
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = StartupConsts.OPEN_API_DESCRIPTION,
                        Name = StartupConsts.OPEN_API_AUTHORIZATION,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = configuration[StartupConsts.BEARER],
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = configuration[StartupConsts.BEARER]
                             },
                        },
                        new List<string>()
                    }
                });

            });
        }
    }
}
