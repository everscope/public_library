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
        public IActionResult AddBook(BookInputModel book)
        {
            Book newBook = _mapper.Map<Book>(book);
            try
            {
                _databaseReader.AddBook(newBook);
                Log.ForContext<BookController>().Information("Book {Book} has been added", book);
                return Ok();
            }
            catch
            {
                Log.ForContext<BookController>().Information("Book {Book} can not be added", book);
                return BadRequest();
            }
        }

        //public IActionResult RemoveBook()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
