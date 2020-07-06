using Investimentos.Application.Interfaces;
using Investimentos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.IoC
{
    public static class DependencyResolver
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IInvestimentoService, InvestimentoService>();
        }
    }
}