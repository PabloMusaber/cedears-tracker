using AutoMapper;
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Models;

namespace CEDEARsTracker.Profiles
{
    public class MovementProfile : Profile
    {
        public MovementProfile()
        {
            // Source -> Target
            CreateMap<Movement, MovementReadDto>()
                .ForMember(dest => dest.Ticker, opt => opt.MapFrom(src => src.Instrument != null ? src.Instrument.Ticker ?? string.Empty : string.Empty));
            CreateMap<MovementCreateDto, Movement>();
        }
    }
}