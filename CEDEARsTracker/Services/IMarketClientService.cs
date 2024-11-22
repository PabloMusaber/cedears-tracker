namespace CEDEARsTracker.Services;

using CEDEARsTracker.Models;

public interface IMarketClientService
{
    Task<string?> GetAuthTokenAsync();
    Task<BalancesAndPositionsResponse> GetBalancesAndPositionsAsync(string accountNumber);
}