using System;
using System.Net.Http;
using Investimentos.Application.Configuration;
using Investimentos.Application.Services;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(RendaFixaServiceUnitTestsCollection))]
    public class RendaFixaServiceUnitTestsCollection : ICollectionFixture<RendaFixaServiceFixture> { }

    public class RendaFixaServiceFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private RendaFixaService _service;

        public RendaFixaService GetService()
        {
            if (_service is null)
            {
                Mocker.Use<HttpClient>(new HttpClient());
                Mocker.Use<IOptions<ApplicationOptions>>(Options.Create(new ApplicationOptions
                {
                    BaseAddress = "http://www.mocky.io",
                    RendaFixaEndpoint = "5e3429a33000008c00d96336"
                }));

                _service = Mocker.CreateInstance<RendaFixaService>();
            }

            return _service;
        }
        
        public void Dispose() { }
    }
}