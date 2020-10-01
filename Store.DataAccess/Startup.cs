using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;
using Store.DataAccess.Repositories.EFRepositories;
using Store.DataAccess.Repositories.Interfaces;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess
{
    public class Startup
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(StartupOptions.Connection), ServiceLifetime.Singleton);
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddTransient<UserManager<User>>();
            services.AddTransient<IAuthorInPrintingEditionRepository<AuthorInPrintingEdition>, AuthorInPrintingEditionRepository>();
            services.AddTransient<IAuthorRepository<Author>, AuthorRepository>();
            services.AddTransient<IOrderItemRepository<OrderItem>, OrderItemRepository>();
            services.AddTransient<IOrderRepository<Order>, OrderRepository>();
            services.AddTransient<IPrintingEditionRepository<PrintingEdition>, PrintingEditionRepository>();

            DataBaseInitialization.Initialize(services);
        }
    }
}
