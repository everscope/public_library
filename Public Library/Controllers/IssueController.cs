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

        [HttpPost("new")]
        public async Task<IActionResult> CreateIssue(IssueInputModel issue)
        {
            try
            {
                await _databaseReader.AddIssue(issue);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Issue> issues = await _databaseReader.GetAllIssues();
                List<IssueDisplayModel> issueDisplayModels = _mapper.Map<List<IssueDisplayModel>>(issues);
                Log.ForContext<IssueController>().Information("Requested all issues.");
                return Ok(issueDisplayModels);
            }
            catch
            {
                Log.ForContext<IssueController>().Error("Requested all issues, returned error.");
                return StatusCode(500);
            }
        }

        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseIssue(int id)
        {
            try
            {
                await _databaseReader.CloseIssue(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
