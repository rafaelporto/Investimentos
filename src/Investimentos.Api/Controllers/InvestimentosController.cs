using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Investimentos.Application.Interfaces;
using System.Threading.Tasks;

namespace Investimentos.Api.Controllers
{
    //TODO implementar Authorization
    [ApiController]
    [Route("[controller]")]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentoService _service;
        private readonly ITesouroDiretoService _tesouroService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundoService _fundoService;
        private readonly ILogger<InvestimentosController> _logger;

        public InvestimentosController(ITesouroDiretoService tesouroService,
                                        IRendaFixaService rendaFixaService,
                                        IFundoService fundoService,
                                        ILogger<InvestimentosController> logger)
        {
            _tesouroService = tesouroService;
            _rendaFixaService = rendaFixaService;
            _fundoService = fundoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(await _tesouroService.GetInvestimentos());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRendaFixa()
        {
            return new JsonResult(await _rendaFixaService.GetRendasFixas());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFundos()
        {
            return new JsonResult(await _fundoService.GetFundos());
        }
    }
}
