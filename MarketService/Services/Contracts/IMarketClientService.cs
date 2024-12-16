using MarketService.Dtos;

namespace MarketService.Services.Interfaces
{
    public interface IMarketClientService
    {
        Task<string?> GetTokenAsync();
        Task<string?> AuthenticateAsync();
        Task<BalancesAndPositionsResponseDto> GetBalancesAndPositionsAsync(string accountNumber);
        Task<CurrentPriceResponseDto> GetCurrentPrice(string ticker, string instrumentType, string settlement);
    }
}