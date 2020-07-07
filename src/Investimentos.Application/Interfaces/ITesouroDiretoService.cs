using System.Collections.Generic;
using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface ITesouroDiretoService
    {
        Task<IEnumerable<InvestimentoModel>> GetInvestimentos();
        Task<Result<TdsModel>> GetTesourosDiretos();
    }
}