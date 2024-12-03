using AutoMapper;
using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Profiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            CreateMap<InstrumentReadDto, InvestmentsReturnsDto>();
        }
    }
}