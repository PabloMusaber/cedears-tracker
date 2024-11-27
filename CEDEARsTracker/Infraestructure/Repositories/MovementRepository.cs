using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CEDEARsTracker.Infraestructure.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly AppDbContext _context;

        public MovementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertMovementAsync(Movement movement)
        {
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movement>> GetByInstrumentAsync(Guid instrumentId)
        {
            return await _context.Movements.Where(c => c.InstrumentId == instrumentId).Include(c => c.Instrument).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid movementId)
        {
            var entity = await _context.Movements.Where(e => e.Id == movementId).SingleOrDefaultAsync();

            if (entity == null)
                return false; // Entity not found

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}