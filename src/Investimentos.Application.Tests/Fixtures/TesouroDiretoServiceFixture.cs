using System;
using System.Net.Http;
using Investimentos.Application.Configuration;
using Investimentos.Application.Services;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace Investimentos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(TesouroDiretoServiceUnitTestsCollection))]
    public class TesouroDiretoServiceUnitTestsCollection : ICollectionFixture<TesouroDiretoServiceFixture> { }

    public class TesouroDiretoServiceFixture : IDisposable
    {
        public readonly AutoMocker Mocker = new AutoMocker();
        const string _localeBogus = "pt_BR";
        private TesouroDiretoService _service;

        public TesouroDiretoService GetService()
        {
            if (_service is null)
            {
                Mocker.Use<HttpClient>(new HttpClient());
                Mocker.Use<IOptions<ApplicationOptions>>(Options.Create(new ApplicationOptions
                {
                    BaseAddress = "http://www.mocky.io",
                    TesouroDiretoEndpoint = "5e3428203000006b00d9632a"
                }));

                _service = Mocker.CreateInstance<TesouroDiretoService>();
            }

            return _service;
        }
        
        public void Dispose() { }
    }
}