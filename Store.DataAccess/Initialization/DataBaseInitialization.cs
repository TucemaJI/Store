using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Initialization
{
    public class DataBaseInitialization
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = "Admin",
                FirstName = "Administrator",
                LastName = "Administaratovich"
            });
        }
    }
}
