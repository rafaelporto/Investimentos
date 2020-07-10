using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using Investimentos.Application.AutoMapperProfiles;
using Investimentos.Application.Models;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(TesouroDiretoMapperUnitTestsCollection))]
    public class TesouroDiretoMapperUnitTestsCollection : ICollectionFixture<TesouroDiretoMapperFixture> { }

    public class TesouroDiretoMapperFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private IMapper _mapper;

        public IMapper GetMapper()
        {
            if (_mapper is null)
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                            {
                                cfg.AddProfile(new TesouroDiretoProfile());
                            });
                _mapper = mapperConfig.CreateMapper();
            }

            return _mapper;
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