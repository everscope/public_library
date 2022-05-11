using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB.Interfaces
{
    public interface IDatabaseReader
    {
        public Task AddPatron(Patron patron);
        public Task DeletePatron(Patron patron);
        public Task DeletePatronById(string id);
        public Task<List<Patron>> GetAllPatrons();
        public Task<Patron> GetPatronById(string id);
        public Task<string> GetPatronId(string name, string surname, string email);
        public Task AddBook(Book book);
        //GetBookId is array because database can contain more than 1 book copy
        public Task<string []> GetBookId(BookInputModel book);
        public Task<List<Book>> GetAllBooks();
        public Task DeleteBook(string id);
        public Task MoveBook(string id, string placement);
        public Task<Book> GetBookById(string id);
        public Task<List<Issue>> GetAllIssues();
        public Task AddIssue();
    }
}
