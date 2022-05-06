using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using System.Net;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("patron/")]
    public class PatronController : ControllerBase
    {

        private readonly ILogger<PatronController> _logger;
        private readonly IMapper _mapper;
        private readonly IDatabaseReader _databaseReader;

        public PatronController(ILogger<PatronController> logger,
                                IMapper mapper,
                                IDatabaseReader databaseReader)
        {
            _logger = logger;
            _mapper = mapper;
            _databaseReader = databaseReader;
        }

        [HttpPost("new")]
        public IActionResult CreateUser(PatronInputModel patron)
        {
            try
            {
                Patron newPatron = _mapper.Map<Patron>(patron);
                _databaseReader.AddPatron(newPatron);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteUser(PatronInputModel patron)
        {
            try
            {
                Patron newPatron = _mapper.Map<Patron>(patron);
                _databaseReader.DeletePatron(newPatron);
                return Ok();
            }
            catch
            {
                return BadRequest("Current patron can not be found");
            }
        }

    }
}