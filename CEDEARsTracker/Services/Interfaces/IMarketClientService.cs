using CEDEARsTracker.Dtos;
using CEDEARsTracker.Models;

namespace CEDEARsTracker.Services.Interfaces
{
    public interface IMarketClientService
    {
        Task<string?> GetTokenAsync();
        Task<string?> AuthenticateAsync();
        Task<BalancesAndPositionsResponse> GetBalancesAndPositionsAsync(string accountNumber);
        Task<CurrentPriceResponseDto> GetCurrentPrice(string ticker, string instrumentType, string settlement);
    }
}