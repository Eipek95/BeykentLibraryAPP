using EntityLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class Context : IdentityDbContext<AppUser, AppRole, string>
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Rental>().HasKey(r => r.RentalId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Rentals)
                .HasForeignKey(r => r.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
