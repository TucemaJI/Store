using Microsoft.AspNetCore.Identity;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;
using System;
using System.Threading.Tasks;

namespace Store.DataAccess.Initialization
{
    public class DataBaseInitialization
    {
        public static async Task InitializeUsersRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync(Enums.UserRole.Admin.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Admin.ToString()));
            }
            if (await roleManager.FindByNameAsync(Enums.UserRole.Client.ToString()) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Enums.UserRole.Client.ToString()));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, FirstName = "Administrator", LastName = "Administratorovich"/*, UserName = adminEmail*/ };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Enums.UserRole.Admin.ToString());
                }
            }
        }

        public static async Task InitializeDBAsync(ApplicationContext db) 
        {
            if (await db.Authors.FindAsync("Troelsen") == null)
            {
                await db.Authors.AddAsync(new Author { CreationData = DateTime.UtcNow, Name = "Troelsen", IsRemoved = false });
            }
            if(await db.PrintingEditions.FindAsync("C# 8.0") == null)
            {
                await db.PrintingEditions.AddAsync(new PrintingEdition
                {
                    CreationData = DateTime.Now,
                    Currency = Enums.Currency.USD,
                    Price = 50,
                    Status = Enums.Status.Unpaid,
                    Type = Enums.PrintingEditionType.Book
                });
            }
        }
    }
}
