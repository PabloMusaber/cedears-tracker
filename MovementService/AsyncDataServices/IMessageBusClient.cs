using MovementService.Dtos;

namespace MovementService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewInstrumentBalance(InstrumentBalancePublishedDto instrumentBalancePublishedDto);
    }
}