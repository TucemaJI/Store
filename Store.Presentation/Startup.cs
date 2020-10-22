using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Store.Presentation.Providers;
using Store.Presentation.Middlewares;
using System;
using Microsoft.OpenApi.Models;
using System.Linq;
using Store.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using static Store.Shared.Constants.Constants;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;

namespace Store.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BusinessLogic.Startup.Initialize(services, Configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = Configuration[JwtOptions.Issuer],
                            ValidAudience = Configuration[JwtOptions.Audience],
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,

                            IssuerSigningKey = new JwtProvider().securityKey,
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(StartupOptions.VersionDocument, new OpenApiInfo
                {
                    Version = StartupOptions.VersionDocument,
                    Title = StartupOptions.TitleApplication
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition(StartupOptions.Bearer,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = StartupOptions.OpenApiDescription,
                        Name = StartupOptions.OpenApiAuthorization,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = StartupOptions.Bearer,
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = StartupOptions.Bearer
                             },
                        },
                        new List<string>()
                    }
                });

            });
            services.AddTransient<JwtProvider>();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = StartupOptions.RootPath; });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(StartupOptions.SwaggerUrl, StartupOptions.SwaggerName);

            });

            app.UseMiddleware<LoggerMiddleware>(loggerFactory);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = StartupOptions.SourcePath;

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(StartupOptions.NpmCommand);
                }
            });
        }
    }
}
