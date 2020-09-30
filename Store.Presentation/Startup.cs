using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = JwtProvider.ISSUER,
                            ValidAudience = JwtProvider.AUDIENCE,

                            ClockSkew = TimeSpan.Zero,

                            IssuerSigningKey = new JwtProvider().GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "ToDoAPI" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            services.AddTransient<JwtProvider>();
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
                c.SwaggerEndpoint("v1/swagger.json", "StoreAPI V1");

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

        }
    }
}
