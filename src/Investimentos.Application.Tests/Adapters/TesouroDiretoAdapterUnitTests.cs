using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Models;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Adapters
{
    [Collection(nameof(TesouroDiretoAdapterUnitTestsCollection))]
    public class TesouroDiretoUnitTests
    {
        private readonly TesouroDiretoAdapterFixture _fixture;

        public TesouroDiretoUnitTests(TesouroDiretoAdapterFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Map TesouroDiretoModel to InvestimentoModel - Valid")]
        [Trait("TesouroDiretoAdapter", "Adapters")]
        public async Task Map_TesouroDiretoModel_to_InvestimentoModel_Valid()
        {
            var model = _fixture.GerarTesouroDiretoModel();
            var adapter = _fixture.GetAdapter();
            var result = adapter.Map(model);

            await ValidateMapObject(model, result);
        }

        [Fact(DisplayName = "Map TesouroDiretoModel to InvestimentoModel - Invalid")]
        [Trait("TesouroDiretoAdapter", "Adapters")]
        public async Task Map_TesouroDiretoModel_to_InvestimentoModel_invalid()
        {
            var models = _fixture.GerarTesouroDiretoModel(2);
            var adapter = _fixture.GetAdapter();
            var result = adapter.Map(models.FirstOrDefault());

            var modelToCompare = models.LastOrDefault();

            result.Should().NotBeNull();
            result.Nome.Should().NotBe(modelToCompare.Nome);
            result.ValorInvestido.Should().NotBe(modelToCompare.ValorInvestido);
            result.ValorTotal.Should().NotBe(modelToCompare.ValorTotal);
            result.Vencimento.Should().NotBe(modelToCompare.Vencimento);
            result.Ir.Should().NotBe(modelToCompare.Ir);
            result.ValorResgate.Should().NotBe(modelToCompare.ValorResgate);
        }

        [Fact(DisplayName = "Map List TesouroDiretoModel to InvestimentoModel - Valid")]
        [Trait("TesouroDiretoAdapter", "Adapters")]
        public async Task Map__List_TesouroDiretoModel_to_InvestimentoModel_Valid()
        {
            var models = _fixture.GerarTesouroDiretoModel(20);
            var adapter = _fixture.GetAdapter();
            var results = adapter.Map(models);

            models.Should().NotBeNull();
            models.Should().NotBeEmpty();
            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
            results.Count().Should().Be(models.Count());
        }

        private async Task ValidateMapObject(TesouroDiretoModel tesouroDireto, InvestimentoModel investimento)
        {

            if (tesouroDireto is null)
                investimento.Should().BeNull();

            else if (investimento is null)
                investimento.Should().BeNull();

            else
            {
                investimento.Nome.Should().Be(tesouroDireto.Nome);
                investimento.ValorInvestido.Should().Be(tesouroDireto.ValorInvestido);
                investimento.ValorTotal.Should().Be(tesouroDireto.ValorTotal);
                investimento.Vencimento.Should().Be(tesouroDireto.Vencimento);
                investimento.Ir.Should().Be(tesouroDireto.Ir);
                investimento.ValorResgate.Should().Be(tesouroDireto.ValorResgate);
            }
        }
    }
}
