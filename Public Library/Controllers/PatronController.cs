using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Serilog;

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
                await _databaseReader.AddPatronAsync(newPatron);
                Log.ForContext<PatronController>().Information("Patron {@Patron} has been created", patron);
                return Ok();

            }
            catch (Exception exception)
            {
                Log.ForContext<PatronController>().Information("Patron {@Patron} can not be created", patron);
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemovePatron(PatronInputModel patron)
        {
            try
            {
                Patron newPatron = _mapper.Map<Patron>(patron);
                await _databaseReader.DeletePatronAsync(newPatron);
                Log.ForContext<PatronController>().Information("Patron {@Patron} has been deleted", patron);
                return Ok();
            }
            catch(Exception exception)
            {
                Log.ForContext<PatronController>().Information("Patron {@Patron} can not be deleted", patron);
                return BadRequest(exception.Message);
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllPatrons()
        {
            try
            {
                List<Patron> patrons = await _databaseReader.GetAllPatronsAsync();
                List<PatronWithMinimalizedBooksAndIssues> patronMin =
                    _mapper.Map<List<PatronWithMinimalizedBooksAndIssues>>(patrons);
                Log.ForContext<PatronController>().Information("Requested all patrons data");
                return Ok(patronMin);
            }
            catch (Exception exception)
            {
                Log.ForContext<PatronController>().Error("Requested all patrons data, returned error");
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatronById(string id)
        {
            try
            {
                Patron patron = await _databaseReader.GetPatronByIdAsync(id);
                PatronWithMinimalizedBooksAndIssues patronMin =
                    _mapper.Map<PatronWithMinimalizedBooksAndIssues>(patron);
                Log.ForContext<PatronController>().Information("Requested patron {patron} by id {id}",
                    patron, id);
                return Ok(patronMin);
            }
            catch (Exception exception)
            {
                Log.ForContext<PatronController>().Information("Requested patron by id {id}, " +
                    "patron was not found");
                return NotFound(exception.Message);
            }
        }

        [HttpGet("{name}/{surname}/{email}")]
        public async Task<IActionResult> GetId(string name, string surname, string email)
        {
            try
            {
                string id = await _databaseReader.GetPatronIdAsync(name, surname, email);
                Log.ForContext<PatronController>().Information("Requested patron Id, returned {id}", id);
                return Ok(id);
            }
            catch(Exception exception)
            {
                Log.ForContext<PatronController>().Information("Requested patron Id, patron {@patron} not found",
                    new PatronInputModel { Name = name, Email = email, Surname = surname});
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemovePatronById(string id)
        {
            try
            {
                await _databaseReader.DeletePatronByIdAsync(id);
                Log.ForContext<PatronController>().Information("Deleted patron with id {id}", id);
                return Ok();
            }
            catch(Exception exception)
            {
                Log.ForContext<PatronController>().Information("Requested deletion of patron with id {id}, " +
                    "patron was not found", id);
                return BadRequest(exception.Message);
            }
        }
    }
}