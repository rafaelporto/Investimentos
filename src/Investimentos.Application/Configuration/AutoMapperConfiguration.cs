using AutoMapper;
using Investimentos.Application.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Investimentos.Application.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(FundoProfile),
                                    typeof(RendaFixaProfile),
                                    typeof(TesouroDiretoProfile));
        }
    }
}