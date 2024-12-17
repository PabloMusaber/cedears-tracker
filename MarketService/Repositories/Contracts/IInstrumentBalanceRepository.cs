using MarketService.Models;

namespace MarketService.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentBalanceRepository
    {
        bool InstrumentBalanceExists(Guid instrumentBalanceId);
        Task CreateInstrumentBalanceAsync(InstrumentBalance instrument);
        IEnumerable<InstrumentBalance> GetAllInstrumentsBalance();
        bool ExternalInstrumentExists(Guid externalInstrumentId);
        Task UpdateInstrumentBalanceASync(InstrumentBalance instrument);
    }
}