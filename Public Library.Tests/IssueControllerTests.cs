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
    public class IssueControllerTests
    {
        [Fact]
        public async void CreateIssue_ShouldReturnOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddIssueAsync(It.IsAny<IssueInputModel>()));

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.CreateIssue(new IssueInputModel());
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void CreateIssue_ShouldReturnStatusCode500()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.AddIssueAsync(It.IsAny<IssueInputModel>()))
                .ThrowsAsync(new Exception());

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.CreateIssue(new IssueInputModel());
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void GetAllIssues_ShouldReturnOkWithResult()
        {
            List<Issue> issues = new()
            {
                new Issue
                {
                    Id = 1,
                    Patron = new(),
                    PatronId = "patron1",
                    Book = new(),
                    BookId = "book1"
                },
                new Issue
                {
                    Id = 2,
                    Patron = new(),
                    PatronId = "patron2",
                    Book = new(),
                    BookId = "book2"
                }
            };
            List<IssueDisplayModel> issuesMin = new()
            {
                new IssueDisplayModel
                {
                    Id = 1,
                    Patron = new(),
                    PatronId = "patron1",
                    Book = new(),
                    BookId = "book1"
                },
                new IssueDisplayModel
                {
                    Id = 2,
                    Patron = new(),
                    PatronId = "patron2",
                    Book = new(),
                    BookId = "book2"
                }
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(p => p.Map<List<IssueDisplayModel>>(issues)).Returns(issuesMin);
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllIssuesAsync()).Returns(Task.FromResult(issues));

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.GetAllIssues();
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(issuesMin);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetAllIssues_ShouldReturnStatusCode500()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.GetAllIssuesAsync()).ThrowsAsync(new Exception());

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.GetAllIssues();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void CloseIssue_ShouldReturnOk()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.CloseIssueAsync(It.IsAny<int>()));

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.CloseIssue(1);
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void CloseIssue_ShouldReturnBadRequest()
        {
            var mapper = new Mock<IMapper>();
            var databaseReader = new Mock<IDatabaseReader>();
            databaseReader.Setup(p => p.CloseIssueAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var issueController = new IssueController(mapper.Object, databaseReader.Object);

            var responce = await issueController.CloseIssue(1);
            var result = responce as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
    }
}
