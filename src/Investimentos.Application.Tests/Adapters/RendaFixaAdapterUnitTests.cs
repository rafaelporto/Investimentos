using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Models;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Adapters
{
    [Collection(nameof(RendaFixaAdapterUnitTestsCollection))]
    public class RendaFixaAdapterUnitTests
    {
        private readonly RendaFixaAdapterFixture _fixture;

        public RendaFixaAdapterUnitTests(RendaFixaAdapterFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Map RendaFixaModel to InvestimentoModel - Valid")]
        [Trait("RendaFixaAdapter", "Adapters")]
        public async Task Map_RendaFixaModel_to_InvestimentoModel_Valid()
        {
            var model = _fixture.GerarRendaFixaModel();
            var adapter = _fixture.GetAdapter();
            var result = adapter.Map(model);

            await ValidateMapObject(model, result);
        }

        [Fact(DisplayName = "Map RendaFixaModel to InvestimentoModel - Invalid")]
        [Trait("RendaFixaAdapter", "Adapters")]
        public async Task Map_RendaFixaModel_to_InvestimentoModel_invalid()
        {
            var models = _fixture.GerarRendaFixaModel(2);
            var adapter = _fixture.GetAdapter();
            var result = adapter.Map(models.FirstOrDefault());

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
        [Trait("RendaFixaAdapter", "Adapters")]
        public async Task Map__List_RendaFixaModel_to_InvestimentoModel_Valid()
        {
            var models = _fixture.GerarRendaFixaModel(20);
            var adapter = _fixture.GetAdapter();
            var results = adapter.Map(models);

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
