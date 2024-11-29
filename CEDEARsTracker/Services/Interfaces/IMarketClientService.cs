using CEDEARsTracker.Models;

namespace CEDEARsTracker.Services.Interfaces
{
    public interface IMarketClientService
    {
        Task<string?> GetAuthTokenAsync();
        Task<BalancesAndPositionsResponse> GetBalancesAndPositionsAsync(string accountNumber);
    }
}