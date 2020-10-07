using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Providers;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.BusinessLogic
{
    public class Startup
    {
        public static void Initialize(IServiceCollection services)
        {
            DataAccess.Startup.Initialize(services);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPrintingEditionService, PrintingEditionService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAuthorService, AuthorService>();

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
        }
    }
}
