using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using System.Net;
namespace Public_Library.DAL
{
    public class DatabaseReader : IDatabaseReader
    {
     
        private PublicLibraryContext _context;

        public DatabaseReader(PublicLibraryContext context)
        {
            _context = context;
        }

        public void AddPatron(Patron patron)
        {
            Patron? patronToCreate = _context.Patrons.FirstOrDefault(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);
            if(patronToCreate != null)
            {
                throw new Exception("Current patron already exists");
            }

            _context.Patrons.Add(patron);
            _context.SaveChanges();
        }

        //returns 1 if succes, returns 0 if error;
        public void DeletePatron(Patron patron)
        {
            Patron patronToDelete = _context.Patrons.Single(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);
            _context.Patrons.Remove(patronToDelete);
            _context.SaveChanges();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void MoveBook()
        {

        }

        public void AddIssue()
        {

        }
    }
}
