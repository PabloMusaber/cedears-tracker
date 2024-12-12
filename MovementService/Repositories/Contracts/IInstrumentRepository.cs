using MovementService.Models;

namespace MovementService.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentRepository
    {
        bool InstrumentExists(Guid instrumentId);
        Task CreateInstrumentAsync(Instrument instrument);
        IEnumerable<Instrument> GetAllInstruments();
        bool ExternalInstrumentExists(Guid externalInstrumentId);
    }
}