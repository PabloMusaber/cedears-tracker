using InstrumentService.Dtos;

namespace InstrumentService.SyncDataServices.Http
{
    public interface IHttpMovementDataClient
    {
        Task SendInstrumentToMovementService(InstrumentReadDto instrumentReadDto);
    }
}