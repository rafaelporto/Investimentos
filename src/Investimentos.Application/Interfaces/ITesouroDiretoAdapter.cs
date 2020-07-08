using System.Collections.Generic;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface ITesouroDiretoAdapter
    {
        InvestimentoModel Map(TesouroDiretoModel model);
        IEnumerable<InvestimentoModel> Map(IEnumerable<TesouroDiretoModel> models);
    }
}