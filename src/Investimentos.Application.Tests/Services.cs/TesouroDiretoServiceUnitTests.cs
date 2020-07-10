using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Services
{
    [Collection(nameof(TesouroDiretoServiceUnitTestsCollection))]
    public class TesouroDiretoServiceUnitTests
    {
        private readonly TesouroDiretoServiceFixture _fixture;

        public TesouroDiretoServiceUnitTests(TesouroDiretoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Get Investimentos - Valid")]
        [Trait("TesouroDiretoService", "Services")]
        public async Task Get_Investimentos_From_Request()
        {
            var service = _fixture.GetService();
            var result = await service.GetInvestimentos();

            result.Count().Should().BeGreaterOrEqualTo(0);
        }
    }
}
