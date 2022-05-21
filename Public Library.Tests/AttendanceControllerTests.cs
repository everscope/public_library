using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public_Library.Controllers;
using Public_Library.LIB.Interfaces;
using Xunit;

namespace Public_Library.Tests
{
    public class AttendanceControllerTests
    {
        [Fact]
        public void IncreaseAmount_ShouldReturnOk()
        {
            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Increase());

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.IncreaseAmount();
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void IncreaseAmount_ShouldReturnStatusCode500()
        {
            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Increase()).Throws(new Exception());

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.IncreaseAmount();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public void DecreaseAmount_ShouldReturnOk()
        {
            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Decrease());

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.DecreaseAmount();
            var result = responce as OkResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void DecreaseAmount_ShouldReturnStatusCode500()
        {
            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Decrease()).Throws(new Exception());

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.DecreaseAmount();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public void GetAmount_ShouldReturnOkWithValue()
        {
            int value = 4;

            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Get()).Returns(value);

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.GetAmount();
            var result = responce as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(value);
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetAmount_ShouldReturnStatusCode500()
        {
            var attendanceAmount = new Mock<IAttendanceAmount>();
            attendanceAmount.Setup(p => p.Get()).Throws(new Exception());

            var attendanceController = new AttendanceController(attendanceAmount.Object);

            var responce = attendanceController.GetAmount();
            var result = responce as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
        }
    }
}
