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
    [CollectionDefinition(nameof(TesouroDiretoAdapterUnitTestsCollection))]
    public class TesouroDiretoAdapterUnitTestsCollection : ICollectionFixture<TesouroDiretoAdapterFixture> { }

    public class TesouroDiretoAdapterFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private TesouroDiretoAdapter _adapter;

        public TesouroDiretoAdapter GetAdapter()
        {
            if (_adapter is null)
                _adapter = Mocker.CreateInstance<TesouroDiretoAdapter>();
            
            return _adapter;
        }

        public TesouroDiretoModel GerarTesouroDiretoModel()
        {
            return GerarTesouroDiretoModel(1).FirstOrDefault();
        }

        public IEnumerable<TesouroDiretoModel> GerarTesouroDiretoModel(int quantidade)
        {
            var fakerObj = new Faker<TesouroDiretoModel>(_localeBogus);

            fakerObj.RuleFor(p => p.DataDeCompra, 
                                        (faker, model) => faker.Date.Past(faker.Random.Number(10)));
            fakerObj.RuleFor(p => p.Indice, 
                                        (faker, model) => faker.Commerce.ProductName());
            fakerObj.RuleFor(p => p.Iof, (faker, model) => faker.Random.Decimal(0, 10));
            fakerObj.RuleFor(p => p.Nome, (faker, model) => faker.Company.CompanyName());
            fakerObj.RuleFor(p => p.Tipo, (faker, model) => faker.Commerce.Department(1));
            fakerObj.RuleFor(p => p.ValorInvestido, (faker, model) => faker.Random.Decimal(500, 3000));
            fakerObj.RuleFor(p => p.ValorTotal, 
                                        (faker, model) => faker.Random.Decimal(model.ValorInvestido, model.ValorInvestido * 2));
            fakerObj.RuleFor(p => p.Vencimento, (faker, model) => faker.Date.Future(faker.Random.Number(0, 10)));

            return fakerObj.Generate(quantidade);
        }

        public void Dispose() { }
    }
}