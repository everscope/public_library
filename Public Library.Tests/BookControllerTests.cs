using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public_Library.Controllers;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Xunit;
namespace Public_Library.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public async void CreateBook_ShouldReturnOk()
        {
            var book = new BookInputModel()
            {
                Author = "author",
                Description = "description",
                Title = "title"
            };
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Book>(book)).Returns(new Book()
            {
                Author = "author",
                Description = "description",
                Title = "title",
                Id = "id"
            });
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddBookAsync(It.IsAny<Book>()));

            BookController bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.CreateBook(book);
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void CreateBook_ShouldReturnBadRequest()
        {
            var book = new BookInputModel()
            {
                Author = "author",
                Description = "description",
                Title = "title"
            };
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Book>(book)).Returns(new Book()
            {
                Author = "author",
                Description = "description",
                Title = "title",
                Id = "id"
            });
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddBookAsync(It.IsAny<Book>()))
                .Throws(new Exception());

            BookController bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.CreateBook(book);
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void RemoveBook_ShouldReturnOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeleteBookAsync(It.IsAny<string>()));

            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.RemoveBook("id");
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void RemoveNotExtistingBook_ShouldReturnBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeleteBookAsync(It.IsAny<string>()))
                .Throws(new Exception());

            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.RemoveBook("id");
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void GetBookId_ReturnsOkWithId()
        {
            string[] id = { "id" };
            var mapper = new Mock<IMapper>();

            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetBookIdAsync(It.IsAny<BookInputModel>()))
                .Returns(Task.FromResult(id));
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.GetBookId("title", "author");
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(id);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetBookId_ReturnsBadRequest()
        {
            var mapper = new Mock<IMapper>();

            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetBookIdAsync(It.IsAny<BookInputModel>()))
                .ThrowsAsync(new Exception());
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.GetBookId("title", "author");
            var result = responce as NotFoundObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnOkWithResult()
        {
            List<BookWithMinimalizedPatronAndIssues> books = new()
            {
                new BookWithMinimalizedPatronAndIssues
                {
                    Author = "author1",
                    Id = "1",
                    Title = "title1"
                },
                new BookWithMinimalizedPatronAndIssues
                {
                    Author = "author2",
                    Id = "2",
                    Title = "title2"
                }
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(mapper => mapper.Map<List<BookWithMinimalizedPatronAndIssues>>
                (It.IsAny<List<Book>>())).Returns(books);
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllBooksAsync())
                .Returns(Task.FromResult(new List<Book>()));
            var bookController = new BookController(mapper.Object,
                databaseReader.Object);

            var responce = await bookController.GetAllBooks();
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(books);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetAllBooks_ReturnsBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllBooksAsync()).ThrowsAsync(new Exception());
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.GetAllBooks();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void MoveBook_ShouldReturnOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.MoveBookAsync(It.IsAny<string>(), It.IsAny<string>()));
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.MoveBook("book", "position");
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void MoveBook_ShouldReturnBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.MoveBookAsync(It.IsAny<string>(),
                It.IsAny<string>())).Throws(new Exception());
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.MoveBook("book", "position");
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void SetBookStatus_ShouldReturnOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.SetBookStateAsync(It.IsAny<string>(),
                It.IsAny<BookState>()));
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.SetBookStatus("id", BookState.OnPlace);
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void SetBookStatus_ShouldReturnBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.SetBookStateAsync(It.IsAny<string>(),
                It.IsAny<BookState>())).ThrowsAsync(new Exception());
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.SetBookStatus("id", BookState.OnPlace);
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void GetBookById_ShouldReturnOkWithResult()
        {
            string id = "bookId";
            Book book = new()
            {
                Id = id,
                Title = "title",
                Author = "author"
            };

            BookWithMinimalizedPatronAndIssues bookMin = new()
            {
                Id = id,
                Title = "title",
                Author = "author"
            };


            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<BookWithMinimalizedPatronAndIssues>(book)).Returns(bookMin);
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetBookByIdAsync(id)).Returns(Task.FromResult(book));
            var bookController = new BookController(mapper.Object, databaseReader.Object);

            var responce = await bookController.GetBookById(id);
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(bookMin);
            result.StatusCode.Should().Be(200);
        }
    }
}
