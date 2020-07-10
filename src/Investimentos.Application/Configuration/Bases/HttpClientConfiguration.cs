using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Investimentos.Application.Configuration.Bases
{
    internal static class HttpClientConfiguration
    {
        
        internal static IHttpClientBuilder AddDefaultHttpClient<TClient, TImplementation>(this IServiceCollection service)
        where TClient : class
        where TImplementation : class, TClient
        {
            return service.AddHttpClient<TClient, TImplementation>()
                            .AddPolicyHandler(GetRetryPolicy())
                            .AddPolicyHandler(GetCircuitBreakerPolicy());
        }
        internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                     (response, time) => Debug.WriteLine($"Fail to execute http request: " +
                        $"{response?.Exception?.Message ?? response?.Result?.ReasonPhrase}" +
                        $"Execute again in {time.TotalSeconds} seconds"));
        }
        internal static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
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