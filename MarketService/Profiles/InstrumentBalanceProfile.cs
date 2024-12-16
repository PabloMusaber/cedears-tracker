using AutoMapper;
using MarketService.Dtos;
using InstrumentBalance = MarketService.Models.InstrumentBalance;

namespace MarketService.Profiles
{
    public class InstrumentBalanceProfile : Profile
    {
        public InstrumentBalanceProfile()
        {
            CreateMap<GrpcInstrumentBalanceModel, InstrumentBalanceCreateDto>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.InstrumentId));
            CreateMap<InstrumentBalanceCreateDto, InstrumentBalance>();
            CreateMap<InstrumentBalance, InstrumentBalanceReadDto>();
        }
    }
}