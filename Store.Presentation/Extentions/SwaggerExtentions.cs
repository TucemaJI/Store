using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(StartupOptions.VERSION, new OpenApiInfo
                {
                    Version = StartupOptions.VERSION,
                    Title = StartupOptions.TITLE_APPLICATION
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition(StartupOptions.BEARER,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = StartupOptions.OPEN_API_DESCRIPTION,
                        Name = StartupOptions.OPEN_API_AUTHORIZATION,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = StartupOptions.BEARER,
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = StartupOptions.BEARER
                             },
                        },
                        new List<string>()
                    }
                });

            });
        }
    }
}
