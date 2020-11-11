using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialization;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext<User>
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
            Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasComputedColumnSql(DatabaseInitializationOptions.FULL_NAME);

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

            builder.InitializeDB();
        }
    }
}
