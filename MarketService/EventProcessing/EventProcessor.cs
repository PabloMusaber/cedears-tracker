using System.Text.Json;
using AutoMapper;
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
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.InstrumentBalancePublished:
                    processInstrumentBalance(message);
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
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void processInstrumentBalance(string instrumentPublishedMessage)
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
    }
}