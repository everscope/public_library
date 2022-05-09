using Microsoft.EntityFrameworkCore;
using Public_Library.LIB;

namespace Public_Library
{
    public class PublicLibraryContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        public PublicLibraryContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>()
                .HasOne(p => p.Patron)
                .WithMany(p => p.Issues)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Patron)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Patron>()
            //    .HasMany(p => p.Books)
            //    .WithOne(p=> p.Patron)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasForeignKey(p => p.Id);


        }
    }
}
