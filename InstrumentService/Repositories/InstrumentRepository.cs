using InstrumentService.Infraestructure.Repositories.Interfaces;
using InstrumentService.Models;
using InstrumentService.Data;

namespace InstrumentService.Infraestructure.Repositories
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