using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Investimentos.Application.Models
{
    public class TotalInvestimentosModel
    {
        ///<summary>
        /// - Valor total dos investimento do cliente
        /// - Valor do tipo decimal
        ///</summary>
        ///<example>22399.597</example>
        public decimal ValorTotal => _investimentos.Sum(p => p.ValorTotal);

        [JsonIgnore]
        private List<InvestimentoModel> _investimentos;

        ///<summary>
        /// - Investimentos do cliente
        ///</summary>
        public IReadOnlyCollection<InvestimentoModel> Investimentos => _investimentos;

        ///<summary>
        /// - Retorna true ou false se o objeto possui investimentos
        ///</summary>
        [JsonIgnore]
        public bool HasInvestimentos => _investimentos.Any();

        public TotalInvestimentosModel(IEnumerable<InvestimentoModel> investimentos)
        {
            _investimentos = investimentos?.ToList() ?? new List<InvestimentoModel>();
        }

        public void AddInvestimento(InvestimentoModel model)
        {
            if (model != null)
                _investimentos.Add(model);
        }

        public void AddInvestimentos(IEnumerable<InvestimentoModel> investimentos)
        {
            if (investimentos != null)
                _investimentos.AddRange(investimentos);
        }
    }
}