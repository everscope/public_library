using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;
using Serilog;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("issue/")]
    public class IssueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseReader _databaseReader;

        public IssueController(IMapper mapper,
                                IDatabaseReader databaseReader)
        {
            _mapper = mapper;
            _databaseReader = databaseReader;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Issue> issues = await _databaseReader.GetAllIssues();
                Log.ForContext<IssueController>().Information("Requested all issues.");
                return Ok(issues);
            }
            catch
            {
                Log.ForContext<IssueController>().Error("Requested all issues, returned error.");
                return StatusCode(500);
            }
        }
    }
}
