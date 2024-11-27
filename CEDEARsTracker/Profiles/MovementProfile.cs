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
            CreateMap<Movement, MovementReadDto>();
            CreateMap<MovementCreateDto, Movement>();
        }
    }
}