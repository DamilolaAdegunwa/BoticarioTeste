using AutoMapper;
using Boticario.Domain.Entities;
using Boticario.WebApi.ViewModel;

namespace Boticario.WebApi.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Revendedor, RevendedorViewModel>().ReverseMap();

            CreateMap<Compra, ComprasViewModel>().ReverseMap();

            CreateMap<Compra, ComprasCalcViewModel>()
                .ForMember(dest => dest.CpfRevendedor, opt => opt
            .MapFrom(src => (src.Revendedor.CPF)))
            .ForMember(dest => dest.ValorCashBack, opt => opt
            .MapFrom(src => (src.CalcularCashBack())))
            .ForMember(dest => dest.PorcentagemCashBack, opt => opt
            .MapFrom(src => (src.PorcentagemAplicada())));
        }
    }
}
