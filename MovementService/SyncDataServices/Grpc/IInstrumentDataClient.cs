using MovementService.Dtos;
using MovementService.Models;

namespace MovementService.SyncDataServices.Grpc
{
    public interface IInstrumentDataClient
    {
        IEnumerable<InstrumentCreateDto>? ReturnAllInstruments();
    }
}