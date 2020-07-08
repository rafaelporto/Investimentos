using System.Collections.Generic;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface IRendaFixaAdapter
    {
        InvestimentoModel Map(RendaFixaModel model);
        IEnumerable<InvestimentoModel> Map(IEnumerable<RendaFixaModel> models);
    }
}