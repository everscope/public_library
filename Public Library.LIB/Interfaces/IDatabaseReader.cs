namespace Public_Library.LIB.Interfaces
{
    public interface IDatabaseReader
    {
        public Task AddPatronAsync(Patron patron);
        public Task DeletePatronAsync(Patron patron);
        public Task DeletePatronByIdAsync(string id);
        public Task<List<Patron>> GetAllPatronsAsync();
        public Task<Patron> GetPatronByIdAsync(string id);
        public Task<string> GetPatronIdAsync(string name, string surname, string email);
        public Task AddBookAsync(Book book);
        //GetBookId is array because database can contain more than 1 book copy
        public Task<string []> GetBookIdAsync(BookInputModel book);
        public Task SetBookStateAsync(string id, BookState bookState);
        public Task<List<Book>> GetAllBooksAsync();
        public Task DeleteBookAsync(string id);
        public Task MoveBookAsync(string id, string placement);
        public Task<Book> GetBookByIdAsync(string id);
        public Task<List<Issue>> GetAllIssuesAsync();
        public Task AddIssueAsync(IssueInputModel issue);
        public Task CloseIssueAsync(int id);
    }
}
