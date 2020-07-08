using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IInvestimentoService
    {
        Task<TotalInvestimentosModel> GetInvestimentos();
    } 
}