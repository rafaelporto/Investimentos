using System;
using System.Collections.Generic;
using Bogus;
using Investimentos.Application.Adapters;
using Investimentos.Application.Models;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [Collection(nameof(FundoAdapterUnitTestsCollection))]
    public class FundoAdapterUnitTestsCollection : ICollectionFixture<FundoAdapterFixture> { }


    public class FundoAdapterFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private FundoAdapter _adapter;

        public FundoAdapter GetAdapter()
        {
            if (_adapter is null)
                _adapter = Mocker.CreateInstance<FundoAdapter>();
            
            return _adapter;
        }

        public IEnumerable<FundoModel> GerarFundoModel(int quantidade)
        {
            var fundoModelFactory = new Faker<FundoModel>(_localeBogus);

            fundoModelFactory.RuleFor(p => p.CapitalInvestido, (faker, model) => faker.Random.Decimal(500, 10000));

            return fundoModelFactory.Generate(quantidade);
        }

        public void Dispose() { }
    }
}