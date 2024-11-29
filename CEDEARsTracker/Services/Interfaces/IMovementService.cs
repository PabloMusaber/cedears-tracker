using CEDEARsTracker.Dtos;

namespace CEDEARsTracker.Services.Interfaces
{
    public interface IMovementService
    {
        Task<MovementReadDto?> InsertMovementAsync(Guid instrumentId, MovementCreateDto movementCreateDto);
        Task<IEnumerable<MovementReadDto>?> GetByInstrumentAsync(Guid instrumentId);
        Task<bool> DeleteAsync(Guid movementId);
    }
}