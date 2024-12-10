using MovementService.Dtos;

namespace MovementService.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<InstrumentReadDto> CreateInstrument(InstrumentCreateDto instrumentCreateDto);
        IEnumerable<InstrumentReadDto> GetAllInstruments();
        bool InstrumentExists(Guid instrumentId);
    }
}