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
    [CollectionDefinition(nameof(RendaFixaAdapterUnitTestsCollection))]
    public class RendaFixaAdapterUnitTestsCollection : ICollectionFixture<RendaFixaAdapterFixture> { }

    public class RendaFixaAdapterFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private RendaFixaAdapter _adapter;

        public RendaFixaAdapter GetAdapter()
        {
            if (_adapter is null)
                _adapter = Mocker.CreateInstance<RendaFixaAdapter>();
            
            return _adapter;
        }

        public RendaFixaModel GerarRendaFixaModel()
        {
            return GerarRendaFixaModel(1).FirstOrDefault();
        }

        public IEnumerable<RendaFixaModel> GerarRendaFixaModel(int quantidade)
        {
            var fakerObj = new Faker<RendaFixaModel>(_localeBogus);

            fakerObj.RuleFor(p => p.CapitalInvestido, (faker, model) => faker.Random.Decimal(200, 3000));
            fakerObj.RuleFor(p => p.CapitalAtual,
                                            (faker, model) => faker.Random.Decimal(model.CapitalInvestido, model.CapitalInvestido * 2));
                            
            fakerObj.RuleFor(p => p.DataOperacao, (faker, model) => faker.Date.Past(4));
            fakerObj.RuleFor(p => p.GuarantidoFGC, (faker, model) => faker.Random.Bool());
            fakerObj.RuleFor(p => p.Indice, (faker, model) => faker.Random.Words(1));
            fakerObj.RuleFor(p => p.Iof, (faker, model) => faker.Random.Decimal(0, 10));
            fakerObj.RuleFor(p => p.Nome, (faker, model) => faker.Company.CompanyName());
            fakerObj.RuleFor(p => p.OutrasTaxas, (faker, model) => faker.Random.Decimal(0.1M, 10M));
            fakerObj.RuleFor(p => p.PrecoUnitario, (faker, model) => faker.Random.Decimal(1000, 2500));
            fakerObj.RuleFor(p => p.Primario, (faker, model) => faker.Random.Bool());
            fakerObj.RuleFor(p => p.Quantidade, (faker, model) => faker.Random.Decimal(1, 50));
            fakerObj.RuleFor(p => p.Taxas, (faker, model) => faker.Random.Decimal(1, 20));
            fakerObj.RuleFor(p => p.Tipo, (faker, model) => faker.Random.Word());
            fakerObj.RuleFor(p => p.Vencimento, (faker, model) => faker.Date.Future(faker.Random.Number(1, 10)));

            return fakerObj.Generate(quantidade);
        }

        public void Dispose() { }
    }
}