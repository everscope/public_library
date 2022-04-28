using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;

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
        public void CreateUser(PatronInputModel patron)
        {
            Patron newPatron = _mapper.Map<Patron>(patron);
            _databaseReader.AddPatron(newPatron);
        }

    }
}