using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.DataAccess.AppContext;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Store.BusinessLogic.Common;
using Store.DataAccess.Initialization;

namespace Store.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public UserManager<User> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public IConfiguration Configuration { get; }

        public static readonly ILoggerFactory FileLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => level == LogLevel.Error)
            .AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt")));
        });

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLoggerFactory(FileLoggerFactory);
                });
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddControllers();
            UserManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
            RoleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            ILogger logger = loggerFactory.CreateLogger("Logger");

           
            DataBaseInitialization.InitializeUsersRolesAsync(UserManager, RoleManager);

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
