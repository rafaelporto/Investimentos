using AutoMapper;
using Investimentos.Application.Models;

namespace Investimentos.Application.AutoMapperProfiles
{
    public class FundoProfile : Profile
    {
        public FundoProfile()
        {
            CreateMap<FundoModel, InvestimentoModel>()
                .ForMember(p => p.ValorInvestido, src => src.MapFrom(p => p.CapitalInvestido))
                .ForMember(p => p.ValorTotal, src => src.MapFrom(p => p.ValorAtual))
                .ForMember(p => p.Vencimento, src => src.MapFrom(p => p.DataResgate))
                .ForMember(p => p.Ir, src => src.MapFrom(p => p.Ir))
                .ForMember(p => p.ValorResgate, src => src.MapFrom(p => p.ValorResgate));
        }
    }
}