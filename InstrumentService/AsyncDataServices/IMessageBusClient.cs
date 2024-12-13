using InstrumentService.Dtos;

namespace InstrumentService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewInstrument(InstrumentPublishedDto instrumentPublishedDto);
    }
}