using MarketService.Dtos;

namespace MarketService.SyncDataServices.Grpc
{
    public interface IInstrumentBalanceDataClient
    {
        IEnumerable<InstrumentBalanceCreateDto>? ReturnAllInstrumentsBalance();
    }
}