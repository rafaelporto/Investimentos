using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Services
{
    [Collection(nameof(FundoServiceUnitTestsCollection))]
    public class FundoServiceUnitTests
    {
        private readonly FundoServiceFixture _fixture;

        public FundoServiceUnitTests(FundoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Get Investimentos - Valid")]
        [Trait("FundoService", "Services")]
        public async Task Get_Investimentos_From_Request()
        {
            var service = _fixture.GetService();
            var result = await service.GetInvestimentos();

            result.Count().Should().BeGreaterOrEqualTo(0);
        }
    }
}
