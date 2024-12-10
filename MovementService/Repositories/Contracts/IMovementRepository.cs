using MovementService.Models;

namespace MovementService.Infraestructure.Repositories.Interfaces
{
    public interface IMovementRepository
    {
        Task InsertMovementAsync(Movement movement);
        Task<IEnumerable<Movement>> GetByInstrumentAsync(Guid instrumentId);
        Task<bool> DeleteAsync(Guid movementId);
    }
}