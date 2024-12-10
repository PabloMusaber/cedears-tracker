using MovementService.Infraestructure.Repositories.Interfaces;
using MovementService.Models;
using MovementService.Data;

namespace MovementService.Infraestructure.Repositories
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly AppDbContext _context;

        public InstrumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool InstrumentExists(Guid instrumentId)
        {
            return _context.Instruments.Any(p => p.Id == instrumentId);
        }

        public async Task CreateInstrumentAsync(Instrument instrument)
        {
            _context.Instruments.Add(instrument);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Instrument> GetAllInstruments()
        {
            return _context.Instruments.ToList();
        }
    }
}