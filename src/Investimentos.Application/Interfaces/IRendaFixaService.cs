using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IRendaFixaService
    {
        Task<Result<LcisModel>> GetRendasFixas();
    }
}