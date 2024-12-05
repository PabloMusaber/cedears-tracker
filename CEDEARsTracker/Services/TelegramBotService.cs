using CEDEARsTracker.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CEDEARsTracker.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IConfiguration _configuration;
        private readonly TelegramBotClient _botClient;
        private readonly string _chatId;
        private readonly ISpreadsheetService _spreadsheetService;

        public TelegramBotService(IConfiguration configuration, ISpreadsheetService spreadsheetService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _botClient = new TelegramBotClient(_configuration["Telegram:BotToken"] ??
                throw new InvalidOperationException("The Telegram:BotToken configuration value is missing."));
            _chatId = _configuration["Telegram:ChatId"] ??
                throw new InvalidOperationException("The Telegram:ChatId configuration value is missing.");
            _spreadsheetService = spreadsheetService;
        }

        public async Task SendInvestmentsReturnsSpreadsheet()
        {
            try
            {
                var spreadsheetBytes = await _spreadsheetService.InvestmentsReturnsSpreadsheet();

                using var memoryStream = new MemoryStream(spreadsheetBytes);

                await _botClient.SendDocument(
                    chatId: _chatId,
                    document: InputFile.FromStream(memoryStream, $"Investment_Returns_{DateTime.Today:yyyy-MM-dd}.xlsx"),
                    caption: "Here is your investments returns report!"
                );
                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}