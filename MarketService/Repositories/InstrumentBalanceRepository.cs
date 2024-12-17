using MarketService.Infraestructure.Repositories.Interfaces;
using MarketService.Models;
using MarketService.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateInstrumentBalanceASync(InstrumentBalance instrument)
        {
            var originalInstrument = await _context.InstrumentsBalance
                .FirstOrDefaultAsync(x => x.ExternalId == instrument.ExternalId);

            if (originalInstrument == null)
            {
                throw new KeyNotFoundException($"Instrument with ExternalId '{instrument.ExternalId}' not found.");
            }

            originalInstrument.Ticker = instrument.Ticker;
            originalInstrument.Description = instrument.Description;
            originalInstrument.Holdings = instrument.Holdings;
            originalInstrument.AveragePurchasePrice = instrument.AveragePurchasePrice;
            originalInstrument.InvestedAmount = instrument.InvestedAmount;
            originalInstrument.InstrumentType = instrument.InstrumentType;

            await _context.SaveChangesAsync();
        }
    }
}