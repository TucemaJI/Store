using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.Presentation.Providers;
using Store.Presentation.Middlewares;
using Store.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static Store.Shared.Constants.Constants;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic;
using Store.Presentation.Extentions;
using Store.BusinessLogic.Services.Interfaces;

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
            services.InitializeBL(Configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.ConfigureAuthentication();
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddControllers();
            services.ConfigureSwagger();            
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = StartupOptions.ROOT_PATH; });
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
                c.SwaggerEndpoint(StartupOptions.SWAGGER_URL, StartupOptions.SWAGGER_NAME);

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
                spa.Options.SourcePath = StartupOptions.SOURCE_PATH;

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(StartupOptions.NPM_COMMAND);
                }
            });
        }
    }
}
