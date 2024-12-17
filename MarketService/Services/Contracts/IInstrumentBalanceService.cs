using MarketService.Dtos;

namespace MarketService.Services.Interfaces
{
    public interface IInstrumentBalanceService
    {
        Task<InstrumentBalanceReadDto> CreateInstrumentBalance(InstrumentBalanceCreateDto instrumentBalanceCreateDto);
        IEnumerable<InstrumentBalanceReadDto> GetAllInstrumentsBalance();
        bool InstrumentBalanceExists(Guid instrumentBalanceId);
        bool ExternalInstrumentExists(Guid externalInstrumentId);
        Task UpdateInstrumentBalance(InstrumentBalanceCreateDto instrumentBalanceCreateDto);
    }
}