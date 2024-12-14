using System.Text.Json;
using AutoMapper;
using MovementService.Dtos;
using MovementService.Services.Interfaces;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.EventProcessing
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
                case EventType.InstrumentPublished:
                    addInstrument(message);
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
                case "Instrument_Published":
                    Console.WriteLine("--> Instrument Published Event Detected");
                    return EventType.InstrumentPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addInstrument(string instrumentPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var instrumentService = scope.ServiceProvider.GetRequiredService<IInstrumentService>();

                var instrumentPublishedDto = JsonSerializer.Deserialize<InstrumentPublishedDto>(instrumentPublishedMessage);

                try
                {
                    var instrument = _mapper.Map<InstrumentCreateDto>(instrumentPublishedDto);
                    if (!instrumentService.ExternalInstrumentExists(instrument.ExternalId))
                    {
                        instrumentService.CreateInstrument(instrument);
                        Console.WriteLine("--> Instrument added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Instrument already exists...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Instrument to DB {ex.Message}");
                }
            }
        }
    }
}