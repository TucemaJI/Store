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
using Store.Shared.Options;
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
            services.Configure<JwtOptions>(Configuration.GetSection(StartupConsts.JWT_OPTIONS));
            services.Configure<EmailOptions>(Configuration.GetSection(StartupConsts.EMAIL_OPTIONS));
            services.Configure<ServiceOptions>(Configuration.GetSection(StartupConsts.SERVICE_OPTIONS));
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection(StartupConsts.CONNECTION_STRINGS));
            StripeConfiguration.ApiKey = Configuration[OrderServiceConsts.API_KEY];

            services.InitializeBL(Configuration);

            services.AddCors();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.ConfigureAuthentication(Configuration);
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddControllers();
            services.ConfigureSwagger(Configuration);
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = Configuration[StartupConsts.ROOT_PATH]; });
            services.AddRazorPages();
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
                c.SwaggerEndpoint(Configuration[StartupConsts.SWAGGER_URL], Configuration[StartupConsts.SWAGGER_NAME]);

            });

            app.UseMiddleware<LoggerMiddleware>(loggerFactory);
            app.UseMiddleware<TokenMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Configuration[StartupConsts.SOURCE_PATH];

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(StartupConsts.NPM_COMMAND);
                }
            });
        }
    }
}
