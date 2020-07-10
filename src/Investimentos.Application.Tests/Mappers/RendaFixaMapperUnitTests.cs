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
    [Collection(nameof(RendaFixaMapperUnitTestsCollection))]
    public class RendaFixaMapperUnitTests
    {
        private readonly RendaFixaMapperFixture _fixture;

        public RendaFixaMapperUnitTests(RendaFixaMapperFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Map RendaFixaModel to InvestimentoModel - Valid")]
        [Trait("RendaFixaMapper", "Mappers")]
        public async Task Map_RendaFixaModel_to_InvestimentoModel_Valid()
        {
            var model = _fixture.GerarRendaFixaModel();
            var mapper = _fixture.GetMapper();
            var result = mapper.Map<InvestimentoModel>(model);

            await ValidateMapObject(model, result);
        }

        [Fact(DisplayName = "Map RendaFixaModel to InvestimentoModel - Invalid")]
        [Trait("RendaFixaMapper", "Mappers")]
        public async Task Map_RendaFixaModel_to_InvestimentoModel_invalid()
        {
            var models = _fixture.GerarRendaFixaModel(2);
            var mapper = _fixture.GetMapper();
            var result = mapper.Map<InvestimentoModel>(models.FirstOrDefault());

            var modelToCompare = models.LastOrDefault();

            result.Should().NotBeNull();
            result.Nome.Should().NotBe(modelToCompare.Nome);
            result.ValorInvestido.Should().NotBe(modelToCompare.CapitalInvestido);
            result.ValorTotal.Should().NotBe(modelToCompare.CapitalAtual);
            result.Vencimento.Should().NotBe(modelToCompare.Vencimento);
            result.Ir.Should().NotBe(modelToCompare.Ir);
            result.ValorResgate.Should().NotBe(modelToCompare.ValorResgate);
        }

        [Fact(DisplayName = "Map List RendaFixaModel to InvestimentoModel - Valid")]
        [Trait("RendaFixaMapper", "Mappers")]
        public async Task Map__List_RendaFixaModel_to_InvestimentoModel_Valid()
        {
            var models = _fixture.GerarRendaFixaModel(20);
            var mapper = _fixture.GetMapper();
            var results = mapper.Map<IEnumerable<InvestimentoModel>>(models);

            models.Should().NotBeNull();
            models.Should().NotBeEmpty();
            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
            results.Count().Should().Be(models.Count());
        }

        private async Task ValidateMapObject(RendaFixaModel rendaFixa, InvestimentoModel investimento)
        {

            if (rendaFixa is null)
                investimento.Should().BeNull();

            else if (investimento is null)
                investimento.Should().BeNull();

            else
            {
                investimento.Nome.Should().Be(rendaFixa.Nome);
                investimento.ValorInvestido.Should().Be(rendaFixa.CapitalInvestido);
                investimento.ValorTotal.Should().Be(rendaFixa.CapitalAtual);
                investimento.Vencimento.Should().Be(rendaFixa.Vencimento);
                investimento.Ir.Should().Be(rendaFixa.Ir);
                investimento.ValorResgate.Should().Be(rendaFixa.ValorResgate);
            }
        }
    }
}
