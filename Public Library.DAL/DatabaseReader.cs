using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
namespace Public_Library.DAL
{
    public class DatabaseReader : IDatabaseReader
    {
     
        private PublicLibraryContext _context;

        public DatabaseReader(IDbContextFactory<PublicLibraryContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public void AddPatron(Patron patron)
        {
            _context.Patrons.Add(patron);
            _context.SaveChanges();
        }

        public void DeletePatron()
        {

        }

        public void AddBook()
        {

        }

        public void MoveBook()
        {

        }

        public void AddIssue()
        {

        }
    }
}
