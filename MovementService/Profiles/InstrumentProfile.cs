using AutoMapper;
using MovementService.Dtos;
using MovementService.Models;

namespace MovementService.Profiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            CreateMap<InstrumentCreateDto, Instrument>();
            CreateMap<Instrument, InstrumentReadDto>();
            CreateMap<GrpcInstrumentModel, InstrumentCreateDto>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.InstrumentId));
            CreateMap<InstrumentPublishedDto, InstrumentCreateDto>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        }
    }
}