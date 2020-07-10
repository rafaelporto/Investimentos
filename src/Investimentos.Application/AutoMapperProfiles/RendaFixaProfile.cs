using AutoMapper;
using Investimentos.Application.Models;

namespace Investimentos.Application.AutoMapperProfiles
{
    public class RendaFixaProfile : Profile
    {
        public RendaFixaProfile()
        {
            CreateMap<RendaFixaModel, InvestimentoModel>()
                .ForMember(p => p.ValorInvestido, src => src.MapFrom(p => p.CapitalInvestido))
                .ForMember(p => p.ValorTotal, src => src.MapFrom(p => p.CapitalAtual))
                .ForMember(p => p.Ir, src => src.MapFrom(p => p.Ir))
                .ForMember(p => p.ValorResgate, src => src.MapFrom(p => p.ValorResgate));
        }
    }
}