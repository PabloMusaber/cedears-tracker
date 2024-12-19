namespace SpreadsheetService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}