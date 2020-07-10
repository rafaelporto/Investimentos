using System.Threading.Tasks;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface ICacheService
    {
        void AddFundos(FundosModel model);
        FundosModel GetFundos();
        void AddTesourosDiretos(TdsModel model);
        TdsModel GetTesourosDiretos();
        void AddRendasFixas(LcisModel model);
        LcisModel GetRendasFixas();
    }
}