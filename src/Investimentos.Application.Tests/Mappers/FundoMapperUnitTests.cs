using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Models;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Mappers
{
    [Collection(nameof(FundoMapperUnitTestsCollection))]
    public class FundoMapperUnitTests
    {
        private readonly FundoMapperFixture _fixture;

        public FundoMapperUnitTests(FundoMapperFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Map FundoModel to InvestimentoModel - Valid")]
        [Trait("FundoMapper", "Mappers")]
        public async Task Map_FundoModel_to_InvestimentoModel_Valid()
        {
            var model = _fixture.GerarFundoModel();
            var mapper = _fixture.GetMapper();
            var result = mapper.Map<InvestimentoModel>(model);

            await ValidateMapObject(model, result);
        }

        [Fact(DisplayName = "Map FundoModel to InvestimentoModel - Invalid")]
        [Trait("FundoMapper", "Mappers")]
        public async Task Map_FundoModel_to_InvestimentoModel_invalid()
        {
            var models = _fixture.GerarFundoModel(2);
            var mapper = _fixture.GetMapper();
            var result = mapper.Map<InvestimentoModel>(models.FirstOrDefault());

            var modelToCompare = models.LastOrDefault();

            result.Should().NotBeNull();
            result.Nome.Should().NotBe(modelToCompare.Nome);
            result.ValorInvestido.Should().NotBe(modelToCompare.CapitalInvestido);
            result.ValorTotal.Should().NotBe(modelToCompare.ValorAtual);
            result.Vencimento.Should().NotBe(modelToCompare.DataResgate);
            result.Ir.Should().NotBe(modelToCompare.Ir);
            result.ValorResgate.Should().NotBe(modelToCompare.ValorResgate);
        }

        [Fact(DisplayName = "Map List FundoModel to InvestimentoModel - Valid")]
        [Trait("FundoMapper", "Mappers")]
        public async Task Map__List_FundoModel_to_InvestimentoModel_Valid()
        {
            var models = _fixture.GerarFundoModel(20);
            var mapper = _fixture.GetMapper();
            var results = mapper.Map<IEnumerable<InvestimentoModel>>(models);

            models.Should().NotBeNull();
            models.Should().NotBeEmpty();
            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
            results.Count().Should().Be(models.Count());
        }

        private async Task ValidateMapObject(FundoModel fundo, InvestimentoModel investimento)
        {

            if (fundo is null)
                investimento.Should().BeNull();

            else if (investimento is null)
                investimento.Should().BeNull();

            else
            {
                investimento.Nome.Should().Be(fundo.Nome);
                investimento.ValorInvestido.Should().Be(fundo.CapitalInvestido);
                investimento.ValorTotal.Should().Be(fundo.ValorAtual);
                investimento.Vencimento.Should().Be(fundo.DataResgate);
                investimento.Ir.Should().Be(fundo.Ir);
                investimento.ValorResgate.Should().Be(fundo.ValorResgate);
            }
        }
    }
}
