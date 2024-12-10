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
        }
    }
}