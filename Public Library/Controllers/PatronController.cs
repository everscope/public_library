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


        [HttpGet("all")]
        public async Task<IActionResult> GetAllPatrons()
        {
            try
            {
                List<Patron> patrons = await _databaseReader.GetAllPatrons();
                Log.ForContext<PatronController>().Information("Requested all patrons data");
                return Ok(patrons);
            }
            catch
            {
                Log.ForContext<PatronController>().Error("Requested all patrons data, returned error");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatronById(string id)
        {
            try
            {
                Patron patron = await _databaseReader.GetPatronById(id);
                Log.ForContext<PatronController>().Information("Requested patron {patron} by id {id}",
                                            patron, id);
                return Ok(patron);
            }
            catch
            {
                Log.ForContext<PatronController>().Information("Requested patron by id {id}, " +
                    "patron was not found");
                return NotFound("Patron was not found");
            }
        }

        [HttpGet("{name}/{surname}/{email}")]
        public async Task<IActionResult> GetId(string name, string surname, string email)
        {
            try
            {
                string id = await _databaseReader.GetPatronId(name, surname, email);
                Log.ForContext<PatronController>().Information("Requested patron Id, returned {id}", id);
                return Ok(id);
            }
            catch
            {
                Log.ForContext<PatronController>().Information("Requested patron Id, patron {@patron} not found",
                                        new PatronInputModel { Name = name, Email = email, Surname = surname});
                return NotFound();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                await _databaseReader.DeletePatronById(id);
                Log.ForContext<PatronController>().Information("Deleted patron with id {id}", id);
                return Ok();
            }
            catch
            {
                Log.ForContext<PatronController>().Information("Requested deletion of patron with id {id}, " +
                    "patron was not found", id);
                return BadRequest();
            }
        }
    }
}