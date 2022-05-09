using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Serilog;
using System.Net;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("patron/")]
    public class PatronController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseReader _databaseReader;

        public PatronController(IMapper mapper,
                                IDatabaseReader databaseReader)
        {
            _mapper = mapper;
            _databaseReader = databaseReader;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreatePatron(PatronInputModel patron)
        {
            try
            {
                Patron newPatron = _mapper.Map<Patron>(patron);
                await _databaseReader.AddPatron(newPatron);
                Log.ForContext<PatronController>().Information("Patron {@Patron} has been created", patron);
                return Ok();

            }
            catch (Exception ex)
            {
                Log.ForContext<PatronController>().Information("Patron {@Patron} can not be created", patron);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePatron(PatronInputModel patron)
        {
            try
            {
                Patron newPatron = _mapper.Map<Patron>(patron);
                await _databaseReader.DeletePatron(newPatron);
                Log.ForContext<PatronController>().Information("Patron {@Patron} has been deleted", patron);
                return Ok();
            }
            catch
            {
                Log.ForContext<PatronController>().Information("Patron {@Patron} can not be deleted", patron);
                return BadRequest("Current patron can not be found");
            }
        }

    }
}