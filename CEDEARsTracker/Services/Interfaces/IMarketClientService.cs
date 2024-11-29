namespace CEDEARsTracker.Services.Interfaces;

using CEDEARsTracker.Models;

public interface IMarketClientService
{
    Task<string?> GetAuthTokenAsync();
    Task<BalancesAndPositionsResponse> GetBalancesAndPositionsAsync(string accountNumber);
}