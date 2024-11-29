using System.Net.Http.Headers;
using System.Text.Json;
using CEDEARsTracker.Models;
using CEDEARsTracker.Services.Interfaces;

namespace CEDEARsTracker.Services
{
    public class MarketClientService : IMarketClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MarketClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient.BaseAddress = new Uri(_configuration["PPI:BaseUrl"] ??
                throw new InvalidOperationException("The PPI:BaseUrl configuration value is missing."));
        }

        public async Task<string?> GetAuthTokenAsync()
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

        public async Task<BalancesAndPositionsResponse> GetBalancesAndPositionsAsync(string accountNumber)
        {
            var token = await GetAuthTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/1.0/Account/BalancesAndPositions?accountNumber={accountNumber}");

            request.Headers.Add("AuthorizedClient", _configuration["PPI:AuthorizedClient"]);
            request.Headers.Add("ClientKey", _configuration["PPI:ClientKey"]);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var balanceResponse = JsonSerializer.Deserialize<BalancesAndPositionsResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return balanceResponse ?? new BalancesAndPositionsResponse();
        }
    }
}