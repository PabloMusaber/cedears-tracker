using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentRepository
    {
        bool InstrumentExists(Guid instrumentId);
        Task<List<AveragePurchasePriceDto>> GetAveragePurchasePriceAsync();
        Task UpdateAveragePurchasePriceAsync(Guid instrumentId, decimal averagePurchasePrice);
    }
}