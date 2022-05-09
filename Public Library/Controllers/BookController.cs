using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddBook(BookInputModel book)
        {
            Book newBook = _mapper.Map<Book>(book);
            try
            {
                await _databaseReader.AddBook(newBook);
                Log.ForContext<BookController>().Information("Book {Book} has been added", book);
                return Ok();
            }
            catch
            {
                Log.ForContext < BookController>().Information("Book {Book} can not be added", book);
                return BadRequest("Error while adding book");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemoveBook(string id)
        {
            try
            {
                await _databaseReader.DeleteBook(id);
                return Ok();
            }
            catch
            {
                return BadRequest("Can not delite this book, probably it does not exists");

            } 
        }

        [HttpGet("getId/{title}/{author}")]
        public async Task<IActionResult> GetBookId(string title, string author)
        {
            try
            {
                BookInputModel book = new() { Title = title, Author = author };
                string[] id = await _databaseReader.GetBookId(book);
                Log.ForContext<BookController>().Information("Requested book {book} id," +
                                                        " returned {id}", book, id);
                return Ok(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<Book> books = await _databaseReader.GetAllBooks();
                return Ok(books);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("move/{id}")]
        public async Task<IActionResult> MoveBook(string id, string position)
        {
            try
            {
                await _databaseReader.MoveBook(id, position);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
