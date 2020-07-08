using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Investimentos.Application.Interfaces;
using System.Threading.Tasks;
using System;
using Investimentos.Api.Models;
using Investimentos.Application.Models;

namespace Investimentos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentoService _service;
        private readonly ITesouroDiretoService _tesouroService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundoService _fundoService;
        private readonly ILogger<InvestimentosController> _logger;

        public InvestimentosController(IInvestimentoService service,
                                        ITesouroDiretoService tesouroService,
                                        IRendaFixaService rendaFixaService,
                                        IFundoService fundoService,
                                        ILogger<InvestimentosController> logger)
        {
            _service = service;
            _tesouroService = tesouroService;
            _rendaFixaService = rendaFixaService;
            _fundoService = fundoService;
            _logger = logger;
        }

        /// <summary>
        /// Get Investimentos 
        /// </summary>
        /// <remarks>
        /// Endpoint que retorna o valor total e os investimentos do cliente
        /// </remarks>
        /// <response code="200">Investimentos retornados com sucesso.</response>
        /// <response code="204">Nenhum investimento localizado.</response>
        /// <response code="500">Ocorreu um erro interno ao buscar os investimentos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(TotalInvestimentosModel), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiBadResult), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> GetInvestimentos()
        {
            try
            {
                var result = await _service.GetInvestimentos();

                if (result.HasInvestimentos)
                    return new OkObjectResult(result);

                return new NoContentResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao buscar os investimentos.");
                return new JsonResult(new ApiBadResult("Ocorreu um erro ao buscar os investimentos.")) { StatusCode = 500 };
            }
        }
    }
}
