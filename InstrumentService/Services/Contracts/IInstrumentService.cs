using InstrumentService.Dtos;


namespace InstrumentService.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<InstrumentReadDto> CreateInstrument(InstrumentCreateDto instrumentCreateDto);
        IEnumerable<InstrumentReadDto> GetAllInstruments();
    }
}