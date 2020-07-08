using System;
using Investimentos.Application.Tests.Fixtures;
using Xunit;

namespace Investimentos.Application.Tests.Adapters
{
    [Collection(nameof(FundoAdapterUnitTestsCollection))]
    public class FundoAdapterUnitTests
    {
        private readonly FundoAdapterFixture _fixture;

        public FundoAdapterUnitTests(FundoAdapterFixture fixture)
        {
            _fixture = fixture;
        }

        
    }
}
