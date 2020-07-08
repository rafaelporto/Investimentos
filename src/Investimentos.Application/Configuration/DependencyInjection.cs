using System;
using System.Diagnostics;
using System.Net.Http;
using Investimentos.Application.Adapters;
using Investimentos.Application.Exceptions;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Investimentos.Application.Configuration
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationOptions>(options =>
            {
                options.BaseAddress = configuration.GetValue<string>("ApplicationConfiguration:BaseAddress") ??
                                                                        throw new ConfigurationNullException("Base address deve ser configurado.");                

                options.TesouroDiretoEndpoint = configuration.GetValue<string>("ApplicationConfiguration:Endpoints:TesouroDiretoEndpoint") ??
                                                                        throw new ConfigurationNullException("Tesouro direto endpoint deve ser configurado.");                

                options.RendaFixaEndpoint = configuration.GetValue<string>("ApplicationConfiguration:Endpoints:RendaFixaEndpoint") ??
                                                                        throw new ConfigurationNullException("Renda fixa endpoint deve ser configurado.");

                options.FundosEndpoint = configuration.GetValue<string>("ApplicationConfiguration:Endpoints:FundosEndpoint") ??
                                                                        throw new ConfigurationNullException("Fundos endpoint deve ser configurado.");
            });
            
            services.AddScoped<ITesouroDiretoAdapter, TesouroDiretoAdapter>();
            services.AddScoped<IRendaFixaAdapter, RendaFixaAdapter>();
            services.AddScoped<IFundoAdapter, FundoAdapter>();

            services.AddScoped<IInvestimentoService, InvestimentoService>();

            services.AddDefaultHttpClient<ITesouroDiretoService, TesouroDiretoService>();
            services.AddDefaultHttpClient<IRendaFixaService, RendaFixaService>();
            services.AddDefaultHttpClient<IFundoService, FundoService>();
        }

        private static IHttpClientBuilder AddDefaultHttpClient<TClient, TImplementation>(this IServiceCollection service)
        where TClient : class
        where TImplementation : class, TClient
        {
            return service.AddHttpClient<TClient, TImplementation>()
                            .ConfigurePrimaryHttpMessageHandler(() =>
                            {
                                return new HttpClientHandler
                                {
                                    ServerCertificateCustomValidationCallback =
                                    (message, cert, chain, errors) => { return true; }
                                };
                            })
                            .AddPolicyHandler(GetRetryPolicy())
                            .AddPolicyHandler(GetCircuitBreakerPolicy());
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                     (response, time) => Debug.WriteLine($"Fail to execute http request: " +
                        $"{response?.Exception?.Message ?? response?.Result?.ReasonPhrase}" +
                        $"Execute again in {time.TotalSeconds} seconds"));
        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(15),
                     (response, time) => Debug.WriteLine($"Fail to execute http request: " +
                        $"{response?.Exception?.Message ?? response?.Result?.ReasonPhrase}" +
                        $"Execute again in {time.TotalSeconds} seconds"),
                        () => Debug.WriteLine("Requests now is opened to receive http requests"));
        }
    }
}