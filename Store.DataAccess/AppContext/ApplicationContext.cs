using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;

namespace Store.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<AuthorInPrintingEdition> AuthorsInPrintingEditions { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasComputedColumnSql("[FirstName] + [LastName]");

            builder.Entity<AuthorInPrintingEdition>()
                .HasKey(t => new { t.AuthorId, t.PrintingEditionId });
            builder.Entity<AuthorInPrintingEdition>()
                .HasOne(ape => ape.PrintingEdition)
                .WithMany(pe => pe.AuthorsInPrintingEdition)
                .HasForeignKey(ape => ape.PrintingEditionId);
            builder.Entity<AuthorInPrintingEdition>()
                .HasOne(ape => ape.Author)
                .WithMany(author => author.AuthorInPrintingEditions)
                .HasForeignKey(ape => ape.AuthorId);

            
            DataBaseInitialization.InitializeDB(builder);
        }
    }
}
