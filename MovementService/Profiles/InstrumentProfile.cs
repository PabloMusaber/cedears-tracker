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
        }
    }
}