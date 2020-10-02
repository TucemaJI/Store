using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Entities;
using Store.Shared.Enums;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Initialization
{
    public class DataBaseInitialization
    {
        public static async void Initialize(IServiceCollection services)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            await InitializeUsersRolesAsync(userManager, roleManager);
        }
        public static async Task InitializeUsersRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Enums.UserRole.Admin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Admin.ToString()));
            }
            if (await roleManager.FindByNameAsync(Enums.UserRole.Client.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Client.ToString()));
            }
            if (await userManager.FindByEmailAsync(DatabaseInitializationOptions.AdminEmail) == null)
            {
                User admin = new User 
                {   
                    Email = DatabaseInitializationOptions.AdminEmail,
                    FirstName = DatabaseInitializationOptions.FirstName,
                    LastName = DatabaseInitializationOptions.LastName,
                    UserName = $"{DatabaseInitializationOptions.FirstName}{DatabaseInitializationOptions.LastName}" ,
                };
                IdentityResult createResult = await userManager.CreateAsync(admin, DatabaseInitializationOptions.Password);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Enums.UserRole.Admin.ToString());
                }
            }
        }

        public static void InitializeDB(ModelBuilder builder)
        {
            var author = new Author {Id = 1, Name = DatabaseInitializationOptions.AuthorName, IsRemoved = false };
            

            var pe = new PrintingEdition
            {
                Id = 1,
                Currency = Enums.Currency.USD,
                Price = 50,
                Type = Enums.PrintingEditionType.Book,
                Title = DatabaseInitializationOptions.BookName,
                Description = DatabaseInitializationOptions.BookDescription,
            };
            var aipe = new AuthorInPrintingEdition { AuthorId = author.Id, PrintingEditionId = pe.Id };
            
            builder.Entity<AuthorInPrintingEdition>().HasData(aipe);
            builder.Entity<Author>().HasData(author);
            builder.Entity<PrintingEdition>().HasData(pe);
        }
    }
}
