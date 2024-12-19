using SpreadsheetService.Dtos;

namespace SpreadsheetService.Services.Interfaces
{
    public interface ITelegramBotService
    {
        Task SendInvestmentsReturnsSpreadsheet(IEnumerable<InstrumentBalanceExportDto> instrumentsBalanceExportDto);
    }
}