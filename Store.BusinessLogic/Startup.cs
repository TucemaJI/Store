using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogic.Mappers;
using Store.DataAccess;
using System;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic
{
    public static class Startup
    {
        public static void InitializeBL(this IServiceCollection services, IConfiguration configuration)
        {
            services.InitializeDA(configuration);


            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith(StartupOptions.SERVICE, StringComparison.OrdinalIgnoreCase)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                );

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith(StartupOptions.MAPPER, StringComparison.OrdinalIgnoreCase)))
                .AsSelf()
                .WithTransientLifetime()
                );

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AuthorProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddTransient<IMapper>(item => mapper);

            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith(StartupOptions.PROVIDER, StringComparison.OrdinalIgnoreCase)))
                .AsSelf()
                .WithTransientLifetime()
                );

        }
    }
}
