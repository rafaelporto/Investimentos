using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IFundoService
    {
        Task<Result<FundosModel>> GetFundos();
    }
}