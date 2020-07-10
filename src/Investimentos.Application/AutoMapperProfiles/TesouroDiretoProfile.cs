using AutoMapper;
using Investimentos.Application.Models;

namespace Investimentos.Application.AutoMapperProfiles
{
    public class TesouroDiretoProfile : Profile
    {
        public TesouroDiretoProfile()
        {
            CreateMap<TesouroDiretoModel, InvestimentoModel>()
                .ForMember(p => p.Ir, src => src.MapFrom(p => p.Ir))
                .ForMember(p => p.ValorResgate, src => src.MapFrom(p => p.ValorResgate));
        }
    }
}