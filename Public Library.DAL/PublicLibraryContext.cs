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
                .HasForeignKey(p => p.PatronId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Issue>()
                .HasOne(p => p.Book)
                .WithMany(p => p.Issues)
                .HasForeignKey(p => p.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Issue>()
                .Navigation(p => p.Patron)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<Issue>()
                .Navigation(p => p.Book)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Patron)
                .WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
