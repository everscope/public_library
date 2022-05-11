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

        public async Task AddPatron(Patron patron)
        {

            Patron? patronToCreate = _context.Patrons.FirstOrDefault(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);

            if (patronToCreate != null)
            {
                throw new Exception("Current patron already exists");
            }

            patron.Id = await GenerateId<Patron>();

            await _context.Patrons.AddAsync(patron);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatron(Patron patron)
        {
            Patron patronToDelete = _context.Patrons.Single(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);
            _context.Patrons.Remove(patronToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatronById(string id)
        {
            var patron = await _context.Patrons.SingleAsync(p => p.Id == id);
            _context.Patrons.Remove(patron);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Patron>> GetAllPatrons()
        {
            return await _context.Patrons.ToListAsync();
        }

        public async Task<Patron> GetPatronById(string id)
        {
            return await _context.Patrons.SingleAsync(p=> p.Id == id);
        }

        public async Task<string> GetPatronId(string name, string surname, string email)
        {
            var patron = await _context.Patrons.SingleAsync(p => p.Surname == surname
                                           && p.Name == name
                                           && p.Email == email);
            return patron.Id;
        }

        public async Task AddBook(Book book)
        {
            book.Id = await GenerateId<Book>();

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(string id)
        {
            var bookToDekete = _context.Books.Single(p=>p.Id == id);
            _context.Books.Remove(bookToDekete);
            await _context.SaveChangesAsync();
        }

        public async Task<string[]> GetBookId(BookInputModel book)
        {
            List<Book> books = await _context.Books.Where(p => p.Title == book.Title
                                        && p.Author == book.Author)
                                        .ToListAsync();

            if(books.Count == 0)
            {
                throw new Exception("This book doesn't exist");
            }

            string[] id = new string[books.Count];

            for (int i = 0; i < books.Count; i++)
            {
                id[i] = books[i].Id;
            }

            return id;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }
        
        public async Task<Book> GetBookById(string id)
        {
            return await _context.Books.SingleAsync(p => p.Id == id);
        }


        public async Task MoveBook(string id, string placement)
        {
            var bookToChange =await _context.Books.SingleAsync(p => p.Id == id);

            bookToChange.Placement = placement;

            await _context.SaveChangesAsync();
        }

        public async Task AddIssue()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Issue>> GetAllIssues()
        {
            return await _context.Issues.ToListAsync();
        }

        private async Task<string> GenerateId<T>() where T : class
        {
            do
            {
                string id = Id.Generate();
                if (await _context.FindAsync<T>(id) == null)
                {
                    return id;
                }

            } while (true);
        }
    }
}
