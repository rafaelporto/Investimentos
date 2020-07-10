using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Bogus;
using Investimentos.Application.Configuration;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Models;
using Investimentos.Application.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(FundoServiceUnitTestsCollection))]
    public class FundoServiceUnitTestsCollection : ICollectionFixture<FundoServiceFixture> { }

    public class FundoServiceFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private FundoService _service;

        public FundoService GetService()
        {
            if (_service is null)
            {
                Mocker.Use<HttpClient>(new HttpClient());
                Mocker.Use<IOptions<ApplicationOptions>>(Options.Create(new ApplicationOptions
                {
                    BaseAddress = "http://www.mocky.io",
                    FundosEndpoint = "5e342ab33000008c00d96342"
                }));

                _service = Mocker.CreateInstance<FundoService>();
            }

            return _service;
        }

        public void Dispose() { }
    }
}