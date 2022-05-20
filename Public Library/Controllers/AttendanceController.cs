using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("attendance/")]
    public class AttendanceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AttendanceAmount _attendanceAmount;

        public AttendanceController(IMapper mapper,
                                AttendanceAmount attendanceAmount)
        {
            _mapper = mapper;
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
            catch
            {
                return StatusCode(500);
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
            catch
            {
                return StatusCode(500);
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
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
