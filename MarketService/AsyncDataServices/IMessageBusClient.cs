using MarketService.Dtos;

namespace MarketService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void SendInstrumentBalanceExport(InstrumentBalanceExportPublishedDto instrumentsBalanceExportPublishedDto);
    }
}