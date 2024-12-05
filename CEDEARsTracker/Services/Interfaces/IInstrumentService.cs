using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task CalculateAveragePurchasePriceAsync(Guid? instrumentId);
        Task<List<InvestmentsReturnsDto>> CalculateInvestmentsReturns();
    }
}