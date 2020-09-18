using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Common;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;

namespace Store.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
               
            //        //var userManager = services.GetRequiredService<UserManager<User>>();
            //        //var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //        //await DataBaseInitialization.InitializeUsersRolesAsync(userManager, rolesManager);
                    
                
               
            //}
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.AddFilter((category, level) => level == LogLevel.Warning))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
