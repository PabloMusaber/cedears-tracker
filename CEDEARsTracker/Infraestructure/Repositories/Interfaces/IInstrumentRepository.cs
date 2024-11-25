namespace CEDEARsTracker.Infraestructure.Repositories.Interfaces
{
    public interface IInstrumentRepository
    {
        bool InstrumentExists(Guid instrumentId);
    }
}