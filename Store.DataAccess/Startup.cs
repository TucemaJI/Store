using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;
using System;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess
{
    public static class Startup
    {
        public static void InitializeDA(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(StartupOptions.CONNECTION)), ServiceLifetime.Singleton);
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith(StartupOptions.REPOSITORY, StringComparison.OrdinalIgnoreCase)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                );


            services.Initialize();
        }
    }
}
