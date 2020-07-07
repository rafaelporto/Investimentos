using System.Collections.Generic;

namespace Investimentos.Application.Models
{
    public class TotalInvestimentosModel
    {
        public decimal ValorTotal { get; set; }
        public IEnumerable<InvestimentoModel> Investimentos { get; set; }
    }
}