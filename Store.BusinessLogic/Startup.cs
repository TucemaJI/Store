using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess;
using Scrutor;

namespace Store.BusinessLogic
{
    public static class Startup
    {
        public static void InitializeBL(this IServiceCollection services, IConfiguration configuration)
        {
            services.InitializeDA(configuration);

            // Makes my app not working
            //services.Scan(scan =>
            //    scan.FromApplicationDependencies()
            //    .AddClasses()
            //    .AsMatchingInterface());
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPrintingEditionService, PrintingEditionService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<OrderItemMapper>();
            services.AddTransient<OrderMapper>();
            services.AddTransient<PaymentMapper>();
            services.AddTransient<PrintingEditionMapper>();
            services.AddTransient<UserMapper>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AuthorProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddTransient<IMapper>(item => mapper);
            services.AddTransient<EmailProvider>();
            services.AddTransient<PasswordProvider>();
        }
    }
}
