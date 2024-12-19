using System.Text.Json;
using AutoMapper;
using MarketService.AsyncDataServices;
using MarketService.Dtos;
using MarketService.Services.Interfaces;
using static MarketService.Enumerations.Enumerations;

namespace MarketService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.InstrumentBalancePublished:
                    ProcessInstrumentBalance(message);
                    break;
                case EventType.RequestInstrumentBalanceExport:
                    await SendInstrumentBalanceExportAsync();
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            if (eventType == null)
            {
                return EventType.Undetermined;
            }

            switch (eventType.Event)
            {
                case "Instrument_Balance_Published":
                    Console.WriteLine("--> Instrument Balance Published Event Detected");
                    return EventType.InstrumentBalancePublished;
                case "Request_Instrument_Balance_Export":
                    Console.WriteLine("--> Request Instrument Balance Export Event Detected");
                    return EventType.RequestInstrumentBalanceExport;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void ProcessInstrumentBalance(string instrumentPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var instrumentService = scope.ServiceProvider.GetRequiredService<IInstrumentBalanceService>();

                var instrumentPublishedDto = JsonSerializer.Deserialize<InstrumentBalancePublishedDto>(instrumentPublishedMessage);

                try
                {
                    var instrument = _mapper.Map<InstrumentBalanceCreateDto>(instrumentPublishedDto);
                    Console.WriteLine(instrumentService.ExternalInstrumentExists(instrument.ExternalId));
                    if (!instrumentService.ExternalInstrumentExists(instrument.ExternalId))
                    {
                        instrumentService.CreateInstrumentBalance(instrument);
                        Console.WriteLine("--> Instrument balance added!");
                    }
                    else
                    {
                        instrumentService.UpdateInstrumentBalance(instrument);
                        Console.WriteLine("--> Instrument balance already exists, updating...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Instrument Balance to DB {ex.Message}");
                }
            }
        }

        private async Task SendInstrumentBalanceExportAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var instrumentService = scope.ServiceProvider.GetRequiredService<IInstrumentBalanceService>();
                var marketService = scope.ServiceProvider.GetRequiredService<IMarketClientService>();
                var messageBusClient = scope.ServiceProvider.GetRequiredService<IMessageBusClient>();

                var instrumentsReadDto = instrumentService.GetAllInstrumentsBalance();

                foreach (var instrument in instrumentsReadDto)
                {
                    var currentPrice = await marketService.GetCurrentPrice(instrument.Ticker, instrument.InstrumentType.ToString(), "A-24HS");
                    instrument.CurrentPrice = currentPrice.Price;
                }
                var instrumentsBalanceExportPublishedDto = new InstrumentBalanceExportPublishedDto();

                instrumentsBalanceExportPublishedDto.InstrumentsBalanceExportDto = _mapper.Map<IEnumerable<InstrumentBalanceExportDto>>(instrumentsReadDto);
                instrumentsBalanceExportPublishedDto.Event = "Instrument_Balance_Export_Published";
                messageBusClient.SendInstrumentBalanceExport(instrumentsBalanceExportPublishedDto);
            }

        }
    }
}