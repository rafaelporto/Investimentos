using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IFundoService
    {
        ///<summary>
        /// Obtem todos os investimentos do tipo Fundo
        ///</summary>
        ///<returns>Enumerable do tipo InvestimentoModel</returns>

        Task<IEnumerable<InvestimentoModel>> GetInvestimentos();
    }
}