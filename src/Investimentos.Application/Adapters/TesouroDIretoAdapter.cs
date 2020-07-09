using Microsoft.Extensions.Logging;
using Investimentos.Application.Models;
using System.Collections.Generic;
using System.Linq;
using Investimentos.Application.Interfaces;
using System;
using Investimentos.Application.Exceptions;

namespace Investimentos.Application.Adapters
{
    public class TesouroDiretoAdapter : ITesouroDiretoAdapter
    {
        private readonly ILogger<TesouroDiretoAdapter> _logger;

        public TesouroDiretoAdapter(ILogger<TesouroDiretoAdapter> logger)
        {
            _logger = logger;
        }

        public InvestimentoModel Map(TesouroDiretoModel model)
        {
            try
            {
                if (model is null)
                {
                    _logger.LogInformation("Parametro do tipo {type} nulo. Method: {method}", nameof(TesouroDiretoModel), nameof(Map));
                    return default;
                }

                var result = new InvestimentoModel
                {
                    Nome = model.Nome,
                    ValorInvestido = model.ValorInvestido,
                    ValorTotal = model.ValorTotal,
                    Vencimento = model.Vencimento,
                    Ir = model.Ir,
                    ValorResgate = model.ValorResgate
                };
                return result;
            }
            catch (Exception e)
            {
                var ex = new AdapterException($"Nao foi possivel adaptar a entidade do tipo {nameof(TesouroDiretoModel)}.", e);
                _logger.LogError(ex, "Ocorreu um erro ao adaptar a entidade {paramName} para {resultName}.",
                                    nameof(TesouroDiretoModel), nameof(InvestimentoModel));
                return default;
            }
        }

        public IEnumerable<InvestimentoModel> Map(IEnumerable<TesouroDiretoModel> models)
        {
            try
            {
                if (models is null)
                {
                    _logger.LogInformation("Parametro do tipo {type} nulo. Method: {method}", nameof(TesouroDiretoModel), nameof(Map));
                    return default;
                }

                var result = models.Select(s => new InvestimentoModel
                {
                    Nome = s.Nome,
                    ValorInvestido = s.ValorInvestido,
                    ValorTotal = s.ValorTotal,
                    Vencimento = s.Vencimento,
                    Ir = s.Ir,
                    ValorResgate = s.ValorResgate
                });

                return result;
            }

            catch (Exception e)
            {
                var ex = new AdapterException($"Nao foi possivel adaptar as entidades do tipo {nameof(TesouroDiretoModel)}.", e);
                _logger.LogError(ex, "Ocorreu um erro ao adaptar as entidades {paramName} para {resultName}.",
                                    nameof(TesouroDiretoModel), nameof(InvestimentoModel));
                return default;
            }
        }
    }
}