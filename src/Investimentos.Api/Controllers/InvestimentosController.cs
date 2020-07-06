using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Investimentos.Api.Controllers
{
    //TODO implementar Authorization
    [ApiController]
    [Route("[controller]")]
    public class InvestimentosController : ControllerBase
    {
        private readonly ILogger<InvestimentosController> _logger;

        public InvestimentosController(ILogger<InvestimentosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new { response = "I'm here!" });
        }
    }
}
