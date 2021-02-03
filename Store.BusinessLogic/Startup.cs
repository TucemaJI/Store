using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Mappers;
using Store.DataAccess;
using System;

namespace Store.BusinessLogic
{
    public static class Startup
    {
        public static void InitializeBL(this IServiceCollection services, IConfiguration configuration)
        {
            services.InitializeDA(configuration);

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                );

            //services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<IAuthorService, AuthorService>();
            //services.AddTransient<IOrderService, OrderService>();
            //services.AddTransient<IPrintingEditionService, PrintingEditionService>();
            //services.AddTransient<IUserService, UserService>();

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("mapper", StringComparison.OrdinalIgnoreCase)))
                .AsSelf()
                .WithTransientLifetime()
                );

            //services.AddTransient<OrderItemMapper>();
            //services.AddTransient<OrderMapper>();
            //services.AddTransient<PaymentMapper>();
            //services.AddTransient<PrintingEditionMapper>();
            //services.AddTransient<UserMapper>();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AuthorProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddTransient<IMapper>(item => mapper);

            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("provider", StringComparison.OrdinalIgnoreCase)))
                .AsSelf()
                .WithTransientLifetime()
                );

            //services.AddTransient<EmailProvider>();
            //services.AddTransient<PasswordProvider>();
        }
    }
}
