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

            if (patronToCreate != null)
            {
                throw new Exception("Current patron already exists");
            }

            patron.Id = GenerateId<Patron>();

            _context.Patrons.Add(patron);
            _context.SaveChanges();
        }

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
            book.Id = GenerateId<Book>();

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void DeleteBook(string id)
        {
            var bookToDekete = _context.Books.Single(p=>p.Id == id);
            _context.Books.Remove(bookToDekete);
            _context.SaveChanges();
        }

        public string[] GetBookId(BookInputModel book)
        {
            List<Book> books = _context.Books.Where(p => p.Title == book.Title
                                        && p.Author == book.Author)
                                        .ToList();

            string[] id = new string[books.Count];

            for (int i = 0; i < books.Count; i++)
            {
                id[i] = books[i].Id;
            }

            return id;
        }

        public void MoveBook()
        {

        }

        public void AddIssue()
        {

        }


        private string GenerateId<T>() where T : class
        {
            do
            {
                string id = Id.Generate();
                if (_context.Find<T>(id) == null)
                {
                    return id;
                }

            } while (true);
        }
    }
}
