using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Serilog;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("book/")]
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseReader _databaseReader;

        public BookController(IMapper mapper,
            IDatabaseReader databaseReader)
        {
            _mapper = mapper;
            _databaseReader = databaseReader;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateBook(BookInputModel book)
        {
            Book newBook = _mapper.Map<Book>(book);
            try
            {
                await _databaseReader.AddBookAsync(newBook);
                Log.ForContext<BookController>().Information("Book {Book} has been added", book);
                return Ok();
            }
            catch
            {
                Log.ForContext <BookController>().Error("Book {Book} can not be added", book);
                return StatusCode(500, "Error while adding book");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemoveBook(string id)
        {
            try
            {
                await _databaseReader.DeleteBookAsync(id);
                Log.ForContext<BookController>().Information("Book with id {id} has been removed", id );
                return Ok();
            }
            catch (Exception exception)
            {
                Log.ForContext<BookController>().Information("Book with id {id} can not be removed," +
                    " probably it does not exists", id);
                return BadRequest(exception.Message);

            } 
        }

        [HttpGet("getId/{title}/{author}")]
        public async Task<IActionResult> GetBookId(string title, string author)
        {
            try
            {
                BookInputModel book = new() { Title = title, Author = author };
                string[] id = await _databaseReader.GetBookIdAsync(book);
                Log.ForContext<BookController>().Information("Requested book {book} id," +
                    " returned {id}", book, id);
                return Ok(id);
            }
            catch (Exception exception)
            {
                Log.ForContext<BookController>().Information("Requested book {book} id," +
                    "book was not found", new BookInputModel() { Title = title, Author = author });
                return NotFound(exception.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<Book> books = await _databaseReader.GetAllBooksAsync();
                List<BookWithMinimalizedPatronAndIssues> booksMin =
                    _mapper.Map<List<BookWithMinimalizedPatronAndIssues>>(books);
                Log.ForContext<BookController>().Information("Requested list of all books");
                return Ok(booksMin);
            }
            catch (Exception exception)
            {
                Log.ForContext<BookController>().Error("Requested list of all books, error occurred");
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("move/{id}")]
        public async Task<IActionResult> MoveBook(string id, string position)
        {
            try
            {
                await _databaseReader.MoveBookAsync(id, position);
                Log.ForContext<BookController>().Information("Book with id {id} was moved to position" +
                    "{position}", id, position);
                return Ok();
            }
            catch (Exception exception)
            {
                Log.ForContext<BookController>().Information("Book with id {id} was not moved to position" +
                    "{position}, probably book with this id does not exists", id, position);
                return BadRequest(exception.Message);
            }
        }
            
        [HttpPut("setStatus/{id}")]
        public async Task<IActionResult> SetBookStatus(string id, BookState bookState)
        {
            try
            {
                await _databaseReader.SetBookStateAsync(id, bookState);
                Log.ForContext<BookController>().Information("Book with id {id}, BookState was changed to" +
                    "{bookState}", id, bookState);
                return Ok();
            }
            catch(Exception exception)
            {
                Log.ForContext<BookController>().Information("Book with id {id}, BookState was tried to" +
                    "modify, book was not found", id);
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            try
            {
                Book book = await _databaseReader.GetBookByIdAsync(id);
                BookWithMinimalizedPatronAndIssues bookMin = _mapper.
                    Map<BookWithMinimalizedPatronAndIssues>(book);
                Log.ForContext<BookController>().Information("Book {book} was requested by id", book);
                return Ok(bookMin);
            }
            catch(Exception exception)
            {
                Log.ForContext<BookController>().Information("Book was requested by id {id} " +
                    "and can not be found", id);
                return NotFound(exception.Message);
            }
        }
    }
}
