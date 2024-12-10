using InstrumentService.Models;

namespace InstrumentService.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentRepository
    {
        bool InstrumentExists(Guid instrumentId);
        Task CreateInstrumentAsync(Instrument instrument);
        IEnumerable<Instrument> GetAllInstruments();
    }
}