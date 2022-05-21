using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB.Interfaces;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("attendance/")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceAmount _attendanceAmount;

        public AttendanceController(IAttendanceAmount attendanceAmount)
        {
            _attendanceAmount = attendanceAmount;
        }

        [HttpPut("increase")]
        public IActionResult IncreaseAmount()
        {
            try
            {
                _attendanceAmount.Increase();
                return Ok();
            }
            catch(Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("decrease")]
        public IActionResult DecreaseAmount()
        {
            try
            {
                _attendanceAmount.Decrease();
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("get")]
        public IActionResult GetAmount()
        {
            try
            {
                int amount = _attendanceAmount.Get();
                return Ok(amount);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
