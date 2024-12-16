using MarketService.Infraestructure.Repositories.Interfaces;
using MarketService.Models;
using MarketService.Data;

namespace MarketService.Infraestructure.Repositories
{
    public class InstrumentBalanceRepository : IInstrumentBalanceRepository
    {
        private readonly AppDbContext _context;

        public InstrumentBalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool InstrumentBalanceExists(Guid instrumentBalanceId)
        {
            return _context.InstrumentsBalance.Any(p => p.Id == instrumentBalanceId);
        }

        public async Task CreateInstrumentBalanceAsync(InstrumentBalance instrument)
        {
            _context.InstrumentsBalance.Add(instrument);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<InstrumentBalance> GetAllInstrumentsBalance()
        {
            return _context.InstrumentsBalance.ToList();
        }

        public bool ExternalInstrumentExists(Guid externalInstrumentId)
        {
            return _context.InstrumentsBalance.Any(p => p.ExternalId == externalInstrumentId);
        }
    }
}