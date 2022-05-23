using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Public_Library.DAL;
using Public_Library.LIB;
using Xunit;

namespace Public_Library.Tests
{
    public class DatabaseReaderTests
    {

        private readonly PublicLibraryContext _context;
        private readonly DatabaseReader _databaseReader;

        public DatabaseReaderTests()
        {
            var optionBuilder = new DbContextOptionsBuilder<PublicLibraryContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase");
            _context = new PublicLibraryContext(optionBuilder.Options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Patrons.AddRange(new List<Patron>
            {
                new () {
                    Name = "Name1",
                    Surname = "Surname1",
                    Email = "Email1",
                    Id = "1",
                    Password = "password1"
                },
                new()
                {
                    Name = "Name3",
                    Surname = "Surname3",
                    Email = "Email3",
                    Id = "3",
                    Password = "password3"
                },
                new()
                {
                    Name = "Name4",
                    Surname = "Surname4",
                    Email = "Email4",
                    Id = "4",
                    Password = "password4"
                },
                new()
                {
                    Name = "Name6",
                    Surname = "Surname6",
                    Email = "Email6",
                    Id = "6",
                    Password = "password6"
                },
                new()
                {
                    Name = "Name9",
                    Surname = "Surname9",
                    Email = "Email9",
                    Id = "9",
                    Password = "password9"
                },
                new()
                {
                    Name = "Name10",
                    Surname = "Surname10",
                    Email = "Email10",
                    Id = "10",
                    Password = "password10"
                }
            });

            _context.Books.AddRange(new List<Book>
            {
                new()
                {
                    Author = "author2",
                    Title = "title2",
                    Id = "2"
                },
                new()
                {
                    Author = "author4",
                    Title = "tilte4",
                    Id ="4"
                },
                new()
                {
                    Author = "author6",
                    Title = "title6",
                    Id = "6"
                },
                new()
                {
                    Author = "author7",
                    Title = "title7",
                    Id = "7"
                },
                new()
                {
                    Author = "author8",
                    Title = "title8",
                    Id ="8"
                },
                new()
                {
                    Author = "author9",
                    Title = "title9",
                    Id = "9"
                },
                new()
                {
                    Author = "author10",
                    Title = "title10",
                    Id = "10"
                }
            });

            _context.Issues.AddRange(new List<Issue>
            {
                new()
                {
                    Patron = new()
                    {
                        Name = "Name11",
                        Surname = "Surname11",
                        Email = "Email11",
                        Id = "11",
                        Password = "password11"
                    },
                    PatronId = "11",
                    Book = new()
                    {
                        Author = "author11",
                        Title = "title11",
                        Id = "11"
                    },
                    BookId = "11",
                    DateTime = DateTime.Now,
                    Id = 11
                }
            });

            _context.SaveChanges();
            

            _databaseReader = new DatabaseReader(_context);
        }

        [Fact]
        public async void AddPatronAsync_NewPatron_AddsPatron()
        {
            Patron patron = new()
            {
                Name = "Name2",
                Surname = "Surname2",
                Email = "Email2",
                Id = "2",
                Password = "password2"
            };

            await _databaseReader.AddPatronAsync(patron);

            _context.Patrons.Should().Contain(patron);
        }

        [Fact]
        public async void AddPatronAsync_ExistringPatron_ThrowsException()
        {
            Patron patron = new()
            {
                Name = "Name1",
                Surname = "Surname1",
                Email = "Email1",
                Id = "1",
                Password = "password1"
            };

            await _databaseReader.Invoking(p => p.AddPatronAsync(patron)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void DeletePatronAsync_ExistingPatron_DeletesPatron()
        {
            Patron patron = new()
            {
                Name = "Name3",
                Surname = "Surname3",
                Email = "Email3",
                Id = "3",
                Password = "password3"
            };

            await _databaseReader.DeletePatronAsync(patron);

            _context.Patrons.Should().NotContain(patron);
        }

        [Fact]
        public async void DeletePatronAsync_NotExistingPatron_ThrowsExceptions()
        {
            Patron patron = new();

            await _databaseReader.Invoking(p => p.DeletePatronAsync(patron)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void DeletePatronByIdAsync_ExistingPatron_DeletesPatron()
        {
            string id = "4";

            await _databaseReader.DeletePatronByIdAsync(id);

            _context.Patrons.Should().NotContain(p => p.Id == id);
        }

        [Fact]
        public async void DeletePatronByIdAsync_NotExistingPatron_ThrowsException()
        {
            string id = "0";

            await _databaseReader.Invoking(p => p.DeletePatronByIdAsync(id)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void GetAllPatronsAsync_ReturnsAllPatrons()
        {
            var patrons = await _databaseReader.GetAllPatronsAsync();

            patrons.Should().BeEquivalentTo(patrons.ToList());
        }

        [Fact]
        public async void GetPatronByIdAsync_ExistingPatron_ReturnsPatron()
        {
            string id = "1";

            var patron = await _databaseReader.GetPatronByIdAsync(id);

            patron.Should().Be(_context.Patrons.First(p => p.Id == id));
        }

        [Fact]
        public async void GetPatronByIdAsync_NotExistingPatron_ThrowsException()
        {
            string id = "0";

            await _databaseReader.Invoking(p => p.GetPatronByIdAsync(id)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void GetPatronIdAsync_ExistingPatron_ReturnsId()
        {
            string name = "Name6",
                surname = "Surname6",
                email = "Email6",
                id = "6";

            string idFromDatabase = await _databaseReader.GetPatronIdAsync(name, surname, email);

            idFromDatabase.Should().Be(id);
        }

        [Fact]
        public async void GetPatronIdAsync_NotExistingPatron_ThrowsException()
        {
            string name = "name0",
                surname = "surname0",
                email = "email0";

            await _databaseReader.Invoking(p => p.GetPatronIdAsync(name, surname, email))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void AddBookAsync_AddsBook()
        {
            Book book = new()
            {
                Author = "author1",
                Title = "title1",
                Id = "1",
            };

            await _databaseReader.AddBookAsync(book);

            _context.Books.Should().Contain(book);
        }

        [Fact]
        public async void DeleteBookAsync_ExistingBook_DeletesBook()
        {
            string id = "2";

            await _databaseReader.DeleteBookAsync(id);

            _context.Books.Should().NotContain(p => p.Id == id);
        }

        [Fact]
        public async void DeleteBookAsync_NotExistingBook_ThrowsException()
        {
            string id = "3";

            await _databaseReader.Invoking(p => p.DeleteBookAsync(id)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void GetBookIdAsync_ExistingBook_ReturnsBookId()
        {
            string[] id = { "4" };
            BookInputModel book = new()
            {
                Author = "author4",
                Title = "tilte4"
            };

            string[] idFromDatabase = await _databaseReader.GetBookIdAsync(book);
            idFromDatabase.Should().BeEquivalentTo(id);
        }

        [Fact]
        public async void GetBookIdAsync_NotExistingBook_ThrowsNotFound()
        {
            await _databaseReader.Invoking(p => p.GetBookIdAsync(new BookInputModel()))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void GetAllBooksAsync_ReturnsAllBooks()
        {
            List<Book> books = await _databaseReader.GetAllBooksAsync();

            books.Should().BeEquivalentTo(_context.Books.ToList());
        }

        [Fact]
        public async void GetBookByIdAsync_ExistingBook_ReturnsBook()
        {
            string id = "6";
            Book book = await _databaseReader.GetBookByIdAsync(id);

            book.Should().Be(_context.Books.First(p => p.Id == id));
        }

        [Fact]
        public async void GetBookByIdAsync_NotExisting_ThrowsException()
        {
            string id = "invalidId";

            await _databaseReader.Invoking(p => p.GetBookByIdAsync(id))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void SetBookStateAsync_ExistingBook_ChangesBookState()
        {
            string id = "7";

            await _databaseReader.SetBookStateAsync(id, BookState.OnService);

            _context.Books.First(p => p.Id == id).BookState.Should().Be(BookState.OnService);
        }

        [Fact]
        public async void SetBookStateAsync_NotExistingBook_ThrowsException()
        {
            string id = "invalidId";

            await _databaseReader.Invoking(p => p.SetBookStateAsync(id, BookState.OnService))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void MoveBookAsync_ExistingBook_MovesBook()
        {
            string id = "8";
            string placement = "place";

            await _databaseReader.MoveBookAsync(id, placement);

            _context.Books.First(p => p.Id == id).Placement.Should().Be(placement);
        }

        [Fact]
        public async void MoveBookAsync_NotExistingBook_ThrowsException()
        {
            string id = "invalidId";
            string placement = "place";

            await _databaseReader.Invoking(p => p.MoveBookAsync(id, placement))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void AddIssueAsync_ExistingPatronAndBook_AddsIssue()
        {
            string id = "9";
            IssueInputModel input = new()
            {
                BookId = id,
                PatronId = id
            };
            await _databaseReader.AddIssueAsync(input);

            _context.Issues.Should().Contain(p => p.PatronId == id
                && p.BookId == id);
            _context.Books.First(p => p.Id == id).Issues.Should().NotBeNull();
            _context.Patrons.First(p => p.Id == id).Issues.Should().NotBeNull();
        }

        [Fact]
        public async void AddIssueAsync_NotExistingPatronAndBook_ThrowsException()
        {
            string id = "invalidId";
            IssueInputModel input = new()
            {
                BookId = id,
                PatronId = id
            };

            await _databaseReader.Invoking(p => p.AddIssueAsync(input))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void CloseIssueAsync_ExistingIssue_ClosesIssue()
        {
            int id = 11;

            await _databaseReader.CloseIssueAsync(id);

            _context.Issues.First(p => p.Id == id).isClosed.Should().BeTrue();
        }

        [Fact]
        public async void CloseIssueAsync_NotExistingIssue_ThrowsException()
        {
            int id = -1;

            await _databaseReader.Invoking(p => p.CloseIssueAsync(id)).Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async void GetAllIssuesAsync_ReturnsIssues()
        {
            List<Issue> issues = await _databaseReader.GetAllIssuesAsync();

            issues.Should().BeEquivalentTo(_context.Issues.ToList());
        }
    }
}
