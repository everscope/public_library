using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.DAL
{
    public class PublicLibraryContextFactory : IDesignTimeDbContextFactory <PublicLibraryContext>
    {
        public PublicLibraryContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(Path.Combine(
                        Directory.GetCurrentDirectory(), "appsettings.json"));
            var configuration = configurationBuilder.Build();
            string connection = configuration.GetConnectionString("DatabaseConnection");

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connection);
            return new PublicLibraryContext(optionsBuilder.Options);
        }
    }
}
