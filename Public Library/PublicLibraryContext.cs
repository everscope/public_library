using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public_Library.LIB;

namespace Public_Library
{
    public class PublicLibraryContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        public PublicLibraryContext(DbContextOptions<PublicLibraryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
