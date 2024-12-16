using System.Net.Http.Headers;
using System.Text.Json;
using MarketService.Dto;
using MarketService.Dtos;
using MarketService.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MarketService.Services
{
    public class MarketClientService : IMarketClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly string _tokenCacheKey = string.Empty;

        public MarketClientService(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient.BaseAddress = new Uri(_configuration["PPI:BaseUrl"] ??
                throw new InvalidOperationException("The PPI:BaseUrl configuration value is missing."));
            _memoryCache = memoryCache;
        }

        public async Task<string?> GetTokenAsync()
        {
            if (_memoryCache.TryGetValue(_tokenCacheKey, out string? token))
            {
                return token;
            }

            token = await AuthenticateAsync();
            var expirationTime = DateTime.UtcNow.AddMinutes(30); // Adjust based on your token's actual expiration time
            _memoryCache.Set(_tokenCacheKey, token, expirationTime);

            return token;
        }

        public async Task<string?> AuthenticateAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/1.0/Account/LoginApi");

            request.Headers.Add("AuthorizedClient", _configuration["PPI:AuthorizedClient"]);
            request.Headers.Add("ClientKey", _configuration["PPI:ClientKey"]);
            request.Headers.Add("ApiKey", _configuration["PPI:ApiKey"]);
            request.Headers.Add("ApiSecret", _configuration["PPI:ApiSecret"]);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            AuthTokenResponse? token = JsonSerializer.Deserialize<AuthTokenResponse>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return token?.AccessToken;
        }

        public async Task<BalancesAndPositionsResponseDto> GetBalancesAndPositionsAsync(string accountNumber)
        {
            var token = await GetTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/1.0/Account/BalancesAndPositions?accountNumber={accountNumber}");

            request.Headers.Add("AuthorizedClient", _configuration["PPI:AuthorizedClient"]);
            request.Headers.Add("ClientKey", _configuration["PPI:ClientKey"]);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var balanceResponse = JsonSerializer.Deserialize<BalancesAndPositionsResponseDto>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return balanceResponse ?? new BalancesAndPositionsResponseDto();
        }

        public async Task<CurrentPriceResponseDto> GetCurrentPrice(string ticker, string instrumentType, string settlement)
        {
            var token = await GetTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/1.0/MarketData/Current?ticker={ticker}&type={instrumentType}&settlement={settlement}");

            request.Headers.Add("AuthorizedClient", _configuration["PPI:AuthorizedClient"]);
            request.Headers.Add("ClientKey", _configuration["PPI:ClientKey"]);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var currentPrice = JsonSerializer.Deserialize<CurrentPriceResponseDto>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return currentPrice ?? new CurrentPriceResponseDto();
        }
    }
}