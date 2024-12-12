using AutoMapper;
using InstrumentService.Dtos;
using InstrumentService.Models;

namespace InstrumentService.Profiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            CreateMap<InstrumentCreateDto, Instrument>();
            CreateMap<Instrument, InstrumentReadDto>();
            CreateMap<InstrumentReadDto, GrpcInstrumentModel>()
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Ticker, opt => opt.MapFrom(src => src.Ticker))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.InstrumentType, opt => opt.MapFrom(src => (int)src.InstrumentType));
        }
    }
}