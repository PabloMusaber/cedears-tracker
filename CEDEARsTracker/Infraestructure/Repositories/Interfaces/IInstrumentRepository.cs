using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentRepository
    {
        bool InstrumentExists(Guid instrumentId);
        Task<List<AveragePurchasePriceDto>> GetAveragePurchasePriceAsync(Guid? instrumentId);
        Task UpdateAveragePurchasePriceAsync(Guid instrumentId, decimal averagePurchasePrice);
        Task<List<InstrumentReadDto>> GetAllInstrumentsAsync();
    }
}