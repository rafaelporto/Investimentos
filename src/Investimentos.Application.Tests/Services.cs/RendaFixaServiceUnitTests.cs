using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Services
{
    [Collection(nameof(RendaFixaServiceUnitTestsCollection))]
    public class RendaFixaServiceUnitTests
    {
        private readonly RendaFixaServiceFixture _fixture;

        public RendaFixaServiceUnitTests(RendaFixaServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Get Investimentos - Valid")]
        [Trait("RendaFixaService", "Services")]
        public async Task Get_Investimentos_From_Request()
        {
            var service = _fixture.GetService();
            var result = await service.GetInvestimentos();

            result.Count().Should().BeGreaterOrEqualTo(0);
        }
    }
}
