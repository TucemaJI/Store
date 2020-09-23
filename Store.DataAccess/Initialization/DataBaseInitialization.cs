using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAccess.Entities;
using System.Threading.Tasks;

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
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync(Enums.Enums.UserRole.Admin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Enums.UserRole.Admin.ToString()));
            }
            if (await roleManager.FindByNameAsync(Enums.Enums.UserRole.Client.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.Enums.UserRole.Client.ToString()));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, FirstName = "Administrator", LastName = "Administratorovich", UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Enums.Enums.UserRole.Admin.ToString());
                }
            }
        }

        public static void InitializeDB(ModelBuilder builder)
        {
            var author = new Author {Id = 1, Name = "Andrew Troelsen", IsRemoved = false };
            

            var pe = new PrintingEdition
            {
                Id = 1,
                Currency = Enums.Enums.Currency.USD,
                Price = 50,
                Type = Enums.Enums.PrintingEditionType.Book,
                Title = "C# 8.0",
                Description = "The new best book for learning programming",
            };
            var aipe = new AuthorInPrintingEdition { AuthorId = author.Id, PrintingEditionId = pe.Id };
            
            builder.Entity<AuthorInPrintingEdition>().HasData(aipe);
            builder.Entity<Author>().HasData(author);
            builder.Entity<PrintingEdition>().HasData(pe);

            

        }
    }
}
