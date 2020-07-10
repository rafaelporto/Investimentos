using Investimentos.Application.Adapters;
using Investimentos.Application.Configuration.Bases;
using Investimentos.Application.Exceptions;
using Investimentos.Application.Interfaces;
using Investimentos.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ICacheService, CacheService>();

            services.AddDefaultHttpClient<ITesouroDiretoService, TesouroDiretoService>();
            services.AddDefaultHttpClient<IRendaFixaService, RendaFixaService>();
            services.AddDefaultHttpClient<IFundoService, FundoService>();
        }
    }
}