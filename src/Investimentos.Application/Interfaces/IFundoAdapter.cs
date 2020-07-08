using System.Collections.Generic;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IFundoAdapter
    {
        InvestimentoModel Map(FundoModel model);
        IEnumerable<InvestimentoModel> Map(IEnumerable<FundoModel> models);
    }
}