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
    public static class Startup
    {
        public static void InitializeDA(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(StartupOptions.CONNECTION)), ServiceLifetime.Singleton);
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<UserManager<User>>();
            services.AddTransient<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPrintingEditionRepository, PrintingEditionRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.Initialize();
        }
    }
}
