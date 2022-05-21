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
    public class PatronControllerTests
    {
        [Fact]
        public async void CreatePatron_NewPatron_ReturnsOk()
        {
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Patron>(It.IsAny<PatronInputModel>()));
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddPatronAsync(It.IsAny<Patron>()));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.CreatePatron(new PatronInputModel());
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void CreatePatron_ExistingPatron_ReturnsBadRequest()
        {
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Patron>(It.IsAny<PatronInputModel>()));
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddPatronAsync(It.IsAny<Patron>()))
                .ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.CreatePatron(new PatronInputModel());
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void RemovePatron_ExistingPatron_ReturnsOk()
        {
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Patron>(It.IsAny<PatronInputModel>()));
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeletePatronAsync(It.IsAny<Patron>()));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.RemovePatron(new PatronInputModel());
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void RemovePatron_NotExistingPatron_ReturnsBadRequest()
        {
            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<Patron>(It.IsAny<PatronInputModel>()));
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeletePatronAsync(It.IsAny<Patron>()))
                .ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.RemovePatron(new PatronInputModel());
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void GetAllPatrons_ReturnsOkWithResult()
        {
            List<Patron> patrons = new()
            {
                new Patron
                {
                    Id = "1",
                    Name = "name1",
                    Surname = "surname1",
                    Email = "email1"
                },
                new Patron
                {
                    Id = "2",
                    Name = "name2",
                    Surname = "surname2",
                    Email = "email2"
                }
            };

            List<PatronWithMinimalizedBooksAndIssues> patronsMin = new()
            {
                new PatronWithMinimalizedBooksAndIssues
                {
                    Id = "1",
                    Name = "name1",
                    Surname = "surname1",
                    Email = "email1"
                },
                new PatronWithMinimalizedBooksAndIssues
                {
                    Id = "2",
                    Name = "name2",
                    Surname = "surname2",
                    Email = "email2"
                }
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<List<PatronWithMinimalizedBooksAndIssues>>(patrons))
                .Returns(patronsMin);
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllPatronsAsync()).Returns(Task.FromResult(patrons));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetAllPatrons();
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(patronsMin);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetAllPatrons_ThrownException_ReturnsStatusCode500()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllPatronsAsync()).ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetAllPatrons();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void GetPatronById_ExistingPatron_ReturnsOkWithReslut()
        {
            string id = "patronId";
            Patron patron = new()
            {
                Id = id,
                Name = "name1",
                Surname = "surname1",
                Email = "email1"
            };
            PatronWithMinimalizedBooksAndIssues patronMin = new()
            {
                Id = id,
                Name = "name1",
                Surname = "surname1",
                Email = "email1"
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<PatronWithMinimalizedBooksAndIssues>(patron))
                .Returns(patronMin);
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetPatronByIdAsync(id)).Returns(Task.FromResult(patron));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetPatronById(id);
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(patronMin);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetPatronById_NotExistingPatron_ReturnsNotFound()
        {
            string id = "patronId";
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetPatronByIdAsync(id)).ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetPatronById(id);
            var result = responce as NotFoundObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetId_ExistingPatron_ReturnsOkWithResult()
        {
            string id = "patronId";
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetPatronIdAsync("name", "surname", "email"))
                .Returns(Task.FromResult(id));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetId("name", "surname", "email");
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(id);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetId_NotExistingPatron_ReturnsNotFound()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetPatronIdAsync("name", "surname", "email"))
                .ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.GetId("name", "surname", "email");
            var result = responce as NotFoundObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void RemovePatronById_ExistingPatron_ReturnsOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeletePatronByIdAsync(It.IsAny<string>()));

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.RemovePatronById("patronId");
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void RemovePatronById_ExistingPatron_ReturnsBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.DeletePatronByIdAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var patronController = new PatronController(mapper.Object, databaseReader.Object);

            var responce = await patronController.RemovePatronById("patronId");
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
    }
}
