using CEDEARsTracker.Dtos;
using CEDEARsTracker.Models;
using CEDEARsTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CEDEARsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : ControllerBase
    {
        private readonly IMarketClientService _marketClientService;

        public MarketController(IMarketClientService marketClientService)
        {
            _marketClientService = marketClientService;
        }

        [HttpGet("balance/{accountNumber}")]
        public async Task<ActionResult<BalancesAndPositionsResponse>> GetBalance(string accountNumber)
        {
            try
            {
                var balance = await _marketClientService.GetBalancesAndPositionsAsync(accountNumber);
                return Ok(balance);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching balance: {ex.Message}");
            }
        }

        [HttpGet("current-price")]
        public async Task<ActionResult<CurrentPriceResponseDto>> GetCurrentPrice(string ticker, string instrumentType, string settlement)
        {
            try
            {
                var currentPrice = await _marketClientService.GetCurrentPrice(ticker, instrumentType, settlement);
                return Ok(currentPrice);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching current price: {ex.Message}");
            }
        }
    }
}