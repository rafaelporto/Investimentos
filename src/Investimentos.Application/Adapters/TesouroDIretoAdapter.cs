using Microsoft.Extensions.Logging;
using Investimentos.Application.Models;
using System.Collections.Generic;
using System.Linq;
using Investimentos.Application.Interfaces;

namespace Investimentos.Application.Adapters
{
    public class TesouroDiretoAdapter : ITesouroDiretoAdapter
    {
        private readonly ILogger<TesouroDiretoAdapter> _logger;

        public TesouroDiretoAdapter(ILogger<TesouroDiretoAdapter> logger)
        {
            _logger = logger;
        }

        public IEnumerable<InvestimentoModel> Map(IEnumerable<TesouroDiretoModel> models)
        {
            return models.Select( s => new InvestimentoModel 
            {
                Nome = s.Nome,
                ValorInvestido = s.valorInvestido,
                ValorTotal = s.ValorTotal,
                Vencimento = s.Vencimento,
            });
        }
    }
}