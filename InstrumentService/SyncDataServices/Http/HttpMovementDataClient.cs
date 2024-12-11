using System.Text;
using System.Text.Json;
using InstrumentService.Dtos;

namespace InstrumentService.SyncDataServices.Http
{
    public class HttpMovementDataClient : IHttpMovementDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpMovementDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendInstrumentToMovementService(InstrumentReadDto instrumentReadDto)
        {
            var httpContent = new StringContent(
               JsonSerializer.Serialize(instrumentReadDto),
               Encoding.UTF8,
               "application/json"
           );

            var response = await _httpClient.PostAsync($"{_configuration["MovementService"]}/test", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to MovementService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to MovementService was NOT OK :(");
            }
        }
    }
}