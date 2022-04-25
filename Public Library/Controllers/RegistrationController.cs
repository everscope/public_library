using Microsoft.AspNetCore.Mvc;

namespace Public_Library.Controllers
{
    [ApiController]
    [Route("[registration]")]
    public class RegistrationController : ControllerBase
    {

        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }


    }
}