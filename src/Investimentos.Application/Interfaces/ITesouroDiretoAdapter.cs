using System.Collections.Generic;
using Investimentos.Application.Models;

namespace Investimentos.Application.Interfaces
{
    public interface ITesouroDiretoAdapter
    {
        IEnumerable<InvestimentoModel> Map(IEnumerable<TesouroDiretoModel> models);
    }
}