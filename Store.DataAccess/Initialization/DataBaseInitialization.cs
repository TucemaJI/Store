using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Entities;
using Store.Shared.Enums;
using Store.Shared.Options;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Initialization
{
    public static class DataBaseInitialization
    {
        public static async void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            await InitializeUsersRolesAsync(userManager, roleManager, configuration);
        }
        private static async Task InitializeUsersRolesAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (await roleManager.FindByNameAsync(Enums.UserRole.Admin.ToString()) is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Admin.ToString()));
            }

            if (await roleManager.FindByNameAsync(Enums.UserRole.Client.ToString()) is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Client.ToString()));
            }

            var options = configuration.GetSection(StartupConsts.EMAIL_OPTIONS).Get<EmailOptions>();

            if (await userManager.FindByEmailAsync(options.AdminEmail) is null)
            {
                var admin = new User
                {
                    Email = options.AdminEmail,
                    FirstName = options.FirstName,
                    LastName = options.LastName,
                    UserName = $"{options.FirstName}{options.LastName}",
                    EmailConfirmed = true,
                };
                var createResult = await userManager.CreateAsync(admin,
                    options.AdminPassword);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Enums.UserRole.Admin.ToString());
                }
            }
        }

        public static void InitializeDB(this ModelBuilder builder)
        {
            var author = new Author { Id = 1, Name = DatabaseInitializationConsts.AUTHOR_NAME, IsRemoved = false };

            var pe = new PrintingEdition
            {
                Id = 1,
                Currency = Enums.CurrencyType.USD,
                Price = 150,
                Type = Enums.PrintingEditionType.Book,
                Title = DatabaseInitializationConsts.BOOK_NAME,
                Description = DatabaseInitializationConsts.BOOK_DESCRIPTION,
            };

            var pes = new PrintingEdition
            {
                Id = 2,
                Currency = Enums.CurrencyType.USD,
                Price = 50,
                Type = Enums.PrintingEditionType.Book,
                Title = "C# 5.0",
                Description = "OLd very good book",
            };
            var aipe = new AuthorInPrintingEdition { AuthorId = author.Id, PrintingEditionId = pe.Id };
            var aipes = new AuthorInPrintingEdition { AuthorId = author.Id, PrintingEditionId = pes.Id };

            builder.Entity<AuthorInPrintingEdition>().HasData(aipe);
            builder.Entity<AuthorInPrintingEdition>().HasData(aipes);
            builder.Entity<Author>().HasData(author);
            builder.Entity<PrintingEdition>().HasData(pe);
            builder.Entity<PrintingEdition>().HasData(pes);

        }
    }
}
