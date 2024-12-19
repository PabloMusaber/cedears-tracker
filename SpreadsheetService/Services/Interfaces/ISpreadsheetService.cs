using SpreadsheetService.Dtos;

namespace SpreadsheetService.Services.Interfaces
{
    public interface ISpreadsheetService
    {
        byte[] InvestmentsReturnsSpreadsheet(IEnumerable<InstrumentBalanceExportDto> instrumentsBalanceExportDto);
    }
}