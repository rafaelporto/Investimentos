using System.Threading.Tasks;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Models;
using Microsoft.Extensions.Logging;

namespace Investimentos.Application.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundoService _fundoService;
        private readonly ILogger _logger;

        public InvestimentoService(ITesouroDiretoService tesouroDiretoService,
                                    IRendaFixaService rendaFixaService,
                                    IFundoService fundoService,
                                     ILogger<InvestimentoService> logger)
        {
            _tesouroDiretoService = tesouroDiretoService;
            _rendaFixaService = rendaFixaService;
            _fundoService = fundoService;
            _logger = logger;
        }

        // public async Task<TotalInvestimentosModel> GetInvestimentos()
        // {
        //     var tds = await _tesouroDiretoService.GetTesourosDiretos();
        //     var lcis = await _rendaFixaService.GetRendasFixas();
        //     var fundos = await _fundoService.GetFundos();


        // }
    }
}