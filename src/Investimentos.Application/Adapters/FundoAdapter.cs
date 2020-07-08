using Microsoft.Extensions.Logging;
using Investimentos.Application.Models;
using System.Collections.Generic;
using System.Linq;
using Investimentos.Application.Interfaces;
using System;
using Investimentos.Application.Exceptions;

namespace Investimentos.Application.Adapters
{
    public class FundoAdapter : IFundoAdapter
    {
        private readonly ILogger<FundoAdapter> _logger;

        public FundoAdapter(ILogger<FundoAdapter> logger)
        {
            _logger = logger;
        }

        public InvestimentoModel Map(FundoModel model)
        {
            try
            {
                var result = new InvestimentoModel
                {
                    Nome = model.Nome,
                    ValorInvestido = model.CapitalInvestido,
                    ValorTotal = model.ValorAtual,
                    Vencimento = model.DataResgate,
                    Ir = model.Ir,
                    ValorResgate = model.ValorResgate
                };
                return result;
            }
            catch (Exception e)
            {
                var ex = new AdapterException($"Nao foi possivel adaptar a entidade do tipo {nameof(FundoModel)}.", e);
                _logger.LogError(ex, "Ocorreu um erro ao adaptar a entidade {paramName} para {resultName}.",
                                    nameof(FundoModel), nameof(InvestimentoModel));
                return default;
            }
        }

        public IEnumerable<InvestimentoModel> Map(IEnumerable<FundoModel> models)
        {
            try
            {
                var result = models.Select(s => new InvestimentoModel
                {
                    Nome = s.Nome,
                    ValorInvestido = s.CapitalInvestido,
                    ValorTotal = s.ValorAtual,
                    Vencimento = s.DataResgate,
                    Ir = s.Ir,
                    ValorResgate = s.ValorResgate
                });

                return result;
            }

            catch (Exception e)
            {
                var ex = new AdapterException($"Nao foi possivel adaptar as entidades do tipo {nameof(FundoModel)}.", e);
                _logger.LogError(ex, "Ocorreu um erro ao adaptar as entidades {paramName} para {resultName}.",
                                    nameof(FundoModel), nameof(InvestimentoModel));
                return default;
            }
        }
    }
}