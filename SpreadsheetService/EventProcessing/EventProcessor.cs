using System.Text.Json;
using AutoMapper;
using SpreadsheetService.Dtos;
using SpreadsheetService.Services.Interfaces;
using static SpreadsheetService.Enumerations.Enumerations;

namespace SpreadsheetService.EventProcessing
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
                case EventType.InstrumentBalanceExportPublished:
                    SendInstrumentsBalanceByTelegram(message);
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
                case "Instrument_Balance_Export_Published":
                    Console.WriteLine("--> Instrument Balance Export Published Event Detected");
                    return EventType.InstrumentBalanceExportPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void SendInstrumentsBalanceByTelegram(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramBotService>();

                var instrumentsBalanceExportPublishedDto = JsonSerializer.Deserialize<InstrumentBalanceExportPublishedDto>(message);

                if (instrumentsBalanceExportPublishedDto == null || instrumentsBalanceExportPublishedDto.InstrumentsBalanceExportDto == null)
                {
                    Console.WriteLine($"--> No InstrumentBalanceExport received...");
                    return;
                }

                try
                {
                    telegramService.SendInvestmentsReturnsSpreadsheet(instrumentsBalanceExportPublishedDto.InstrumentsBalanceExportDto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Instrument to DB {ex.Message}");
                }
            }
        }

    }
}