using Microsoft.EntityFrameworkCore;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;

namespace Public_Library.DAL
{
    public class DatabaseReader : IDatabaseReader
    {
        private PublicLibraryContext _context;

        public DatabaseReader(PublicLibraryContext context)
        {
            _context = context;
        }

        public async Task AddPatronAsync(Patron patron)
        {

            Patron? patronToCreate = _context.Patrons.FirstOrDefault(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);

            if (patronToCreate != null)
            {
                throw new Exception("Current patron already exists");
            }

            patron.Id = await GenerateIdAsync<Patron>();

            await _context.Patrons.AddAsync(patron);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatronAsync(Patron patron)
        {
            Patron patronToDelete = _context.Patrons.Single(p => p.Email == patron.Email
                                       && p.Name == patron.Name
                                       && p.Surname == patron.Surname
                                       && p.Password == patron.Password);
            _context.Patrons.Remove(patronToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatronByIdAsync(string id)
        {
            var patron = await _context.Patrons.SingleAsync(p => p.Id == id);
            _context.Patrons.Remove(patron);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Patron>> GetAllPatronsAsync()
        {
            return await _context.Patrons.Include(p => p.Books).Include(p => p.Issues)
                .ToListAsync();
        }

        public async Task<Patron> GetPatronByIdAsync(string id)
        {
            return await _context.Patrons.Include(p => p.Issues).Include(p => p.Books)
                .SingleAsync(p=> p.Id == id);
        }

        public async Task<string> GetPatronIdAsync(string name, string surname, string email)
        {
            var patron = await _context.Patrons.SingleAsync(p => p.Surname == surname
                                           && p.Name == name
                                           && p.Email == email);
            return patron.Id;
        }

        public async Task AddBookAsync(Book book)
        {
            book.Id = await GenerateIdAsync<Book>();

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string id)
        {
            var bookToDekete = _context.Books.Single(p=>p.Id == id);
            _context.Books.Remove(bookToDekete);
            await _context.SaveChangesAsync();
        }

        public async Task<string[]> GetBookIdAsync(BookInputModel book)
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

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(p => p.Issues).Include(p => p.Patron)
                .ToListAsync();
        }
        
        public async Task<Book> GetBookByIdAsync(string id)
        {
            return await _context.Books.Include(p => p.Issues).Include(p => p.Patron)
                .SingleAsync(p => p.Id == id);
        }

        public async Task SetBookStateAsync(string id, BookState bookState)
        {
            var book = await _context.Books.SingleAsync(p => p.Id == id);
            book.BookState = bookState;
            await _context.SaveChangesAsync();
        }

        public async Task MoveBookAsync(string id, string placement)
        {
            var bookToChange =await _context.Books.SingleAsync(p => p.Id == id);

            bookToChange.Placement = placement;

            await _context.SaveChangesAsync();
        }

        public async Task AddIssueAsync(IssueInputModel issue)
        {
            Book book = await _context.Books.SingleAsync(p => p.Id == issue.BookId);
            Patron patron = await _context.Patrons.SingleAsync(p => p.Id == issue.PatronId);

            Issue issueToAdd = new() { Patron = patron, Book = book };
            book.Patron = patron;
            book.BookState = BookState.Borrowed;
            book.IsBorrowed = true;

            await _context.Issues.AddAsync(issueToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task CloseIssueAsync(int id)
        {
            var issue = await _context.Issues.Include(p => p.Patron).Include(p => p.Book)
                .SingleAsync(p => p.Id == id);
            issue.isClosed = true;
            issue.ReturnDateTime = DateTime.Now;
            issue.Book.BookState = BookState.OnService;
            issue.Book.IsBorrowed = false;
            issue.Book.Patron = null;
            issue.Patron.Books.Remove(issue.Book);
            if((issue.DateTime - issue.ReturnDateTime).Days > issue.AllowedDaysAmount)
            {
                issue.isExpired = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Issue>> GetAllIssuesAsync()
        {
            return await _context.Issues.Include(p => p.Patron).Include(p =>p.Book).ToListAsync();
        }

        private async Task<string> GenerateIdAsync<T>() where T : class
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
