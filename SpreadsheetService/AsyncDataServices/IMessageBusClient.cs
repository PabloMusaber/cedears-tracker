using SpreadsheetService.Dtos;

namespace SpreadsheetService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void RequestInstrumentBalanceExport(RequestInstrumentBalanceExportDto request);
    }
}