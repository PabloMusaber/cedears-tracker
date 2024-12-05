using CEDEARsTracker.Dtos;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CEDEARsTracker.Infraestructure.Repositories
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

        public async Task<List<AveragePurchasePriceDto>> GetAveragePurchasePriceAsync(Guid? instrumentId)
        {
            var averagePurchasePrices = await _context.Movements
                .Where(m => m.MovementType == 'B'
                    && (instrumentId == null || m.InstrumentId == instrumentId)) // Filter buy movements
                .GroupBy(m => m.InstrumentId)
                .Select(g => new AveragePurchasePriceDto
                {
                    InstrumentId = g.Key,
                    AveragePurchasePrice = g.Sum(m => m.Price) / g.Count()
                })
                .ToListAsync();

            return averagePurchasePrices;
        }

        public async Task UpdateAveragePurchasePriceAsync(Guid instrumentId, decimal averagePurchasePrice)
        {
            var instrument = await _context.Instruments.FindAsync(instrumentId);
            if (instrument == null)
            {
                return;
            }

            instrument.AveragePurchasePrice = averagePurchasePrice;
            await _context.SaveChangesAsync();
        }

        public async Task<List<InstrumentReadDto>> GetAllInstrumentsAsync()
        {
            var instrumentDtos = await _context.Instruments
                .GroupJoin(
                    _context.Movements,
                    instrument => instrument.Id,
                    movement => movement.InstrumentId,
                    (instrument, movements) => new { instrument, movements }
                )
                .Select(g => new InstrumentReadDto
                {
                    Id = g.instrument.Id,
                    Ticker = g.instrument.Ticker,
                    Description = g.instrument.Description,
                    InstrumentType = g.instrument.InstrumentType,
                    AveragePurchasePrice = g.instrument.AveragePurchasePrice,
                    InvestedAmount = g.movements
                        .Where(m => m.MovementType == 'B')
                        .Sum(m => m.Price * m.Quantity),
                    Holdings = g.movements
                        .Sum(m => m.MovementType == 'B' ? m.Quantity : -m.Quantity)
                })
                .ToListAsync();

            return instrumentDtos;
        }
    }
}