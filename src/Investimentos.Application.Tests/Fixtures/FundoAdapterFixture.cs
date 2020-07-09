using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Investimentos.Application.Adapters;
using Investimentos.Application.Models;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(FundoAdapterUnitTestsCollection))]
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

        public FundoModel GerarFundoModel()
        {
            return GerarFundoModel(1).FirstOrDefault();
        }

        public IEnumerable<FundoModel> GerarFundoModel(int quantidade)
        {
            var fundoModelFactory = new Faker<FundoModel>(_localeBogus);

            fundoModelFactory.RuleFor(p => p.CapitalInvestido, (faker, model) => faker.Random.Decimal(200, 10000));
            fundoModelFactory.RuleFor(p => p.DataCompra, 
                                        (faker, model) => faker.Date.Past(faker.Random.Number(10)));
            fundoModelFactory.RuleFor(p => p.DataResgate, 
                                        (faker, model) => faker.Date.Between(model.DataCompra,
                                                            model.DataCompra.AddYears(faker.Random.Number(10))));
            fundoModelFactory.RuleFor(p => p.Iof, (faker, model) => faker.Random.Decimal(0, 10));
            fundoModelFactory.RuleFor(p => p.Nome, (faker, model) => faker.Company.CompanyName());
            fundoModelFactory.RuleFor(p => p.Quantity, (faker, model) => faker.Random.Decimal(0.1M, 10M));
            fundoModelFactory.RuleFor(p => p.TotalTaxas, (faker, model) => faker.Random.Decimal(0, 500));
            fundoModelFactory.RuleFor(p => p.ValorAtual, 
                                        (faker, model) => faker.Random.Decimal(model.CapitalInvestido, model.CapitalInvestido * 2));

            return fundoModelFactory.Generate(quantidade);
        }

        public void Dispose() { }
    }
}