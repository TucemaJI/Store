using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.Presentation.Extentions;
using Store.Presentation.Middlewares;
using Stripe;
using System.IdentityModel.Tokens.Jwt;
using static Store.Shared.Constants.Constants;

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
            StripeConfiguration.ApiKey = OrderServiceOptions.API_KEY;
            services.InitializeBL(Configuration);



            services.AddCors();
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

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
