namespace MarketService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}