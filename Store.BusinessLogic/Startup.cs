using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.BusinessLogic
{
    public class Startup
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Startup.Initialize(services, configuration);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthorService, AuthorService>();
        }
    }
}
