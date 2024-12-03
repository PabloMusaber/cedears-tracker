using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task CalculateAveragePurchasePriceAsync();
        Task<List<InvestmentsReturnsDto>> CalculateInvestmentsReturns();
    }
}