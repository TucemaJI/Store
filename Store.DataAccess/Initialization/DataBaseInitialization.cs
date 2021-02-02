using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Entities;
using Store.Shared.Enums;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Initialization
{
    public static class DataBaseInitialization 
    {
        public static async void Initialize(this IServiceCollection services)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            await InitializeUsersRolesAsync(userManager, roleManager);
        }
        private static async Task InitializeUsersRolesAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Enums.UserRole.Admin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Admin.ToString()));
            }

            if (await roleManager.FindByNameAsync(Enums.UserRole.Client.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Client.ToString()));
            }

            if (await userManager.FindByEmailAsync(DatabaseInitializationOptions.ADMIN_EMAIL) == null)
            {
                User admin = new User 
                {   
                    Email = DatabaseInitializationOptions.ADMIN_EMAIL,
                    FirstName = DatabaseInitializationOptions.FIRST_NAME,
                    LastName = DatabaseInitializationOptions.LAST_NAME,
                    UserName = $"{DatabaseInitializationOptions.FIRST_NAME}{DatabaseInitializationOptions.LAST_NAME}" ,
                };
                IdentityResult createResult = await userManager.CreateAsync(admin,
                    DatabaseInitializationOptions.PASSWORD);

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Enums.UserRole.Admin.ToString());
                }
            }
        }

        public static void InitializeDB(this ModelBuilder builder)
        {
            var author = new Author {Id = 1, Name = DatabaseInitializationOptions.AUTHOR_NAME, IsRemoved = false };

            var pe = new PrintingEdition
            {
                Id = 1,
                ReturnedCurrency = Enums.CurrencyType.USD,
                Price = 150,
                Type = Enums.PrintingEditionType.Book,
                Title = DatabaseInitializationOptions.BOOK_NAME,
                Description = DatabaseInitializationOptions.BOOK_DESCRIPTION,
            };

            var pes = new PrintingEdition
            {
                Id = 2,
                ReturnedCurrency = Enums.CurrencyType.USD,
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
