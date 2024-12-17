using MovementService.Infraestructure.Repositories.Interfaces;
using MovementService.Models;
using MovementService.Data;
using Microsoft.EntityFrameworkCore;
using MovementService.Dtos;

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

        public bool ExternalInstrumentExists(Guid externalInstrumentId)
        {
            return _context.Instruments.Any(p => p.ExternalId == externalInstrumentId);
        }

        public async Task<List<InstrumentReadDto>> GetAllInstrumentsBalanceAsync()
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
                    ExternalId = g.instrument.ExternalId,
                    Ticker = g.instrument.Ticker,
                    Description = g.instrument.Description,
                    InstrumentType = g.instrument.InstrumentType,
                    AveragePurchasePrice = g.movements
                        .Where(m => m.MovementType == 'B')
                        .Select(m => m.Price).Average(),
                    InvestedAmount = g.movements
                        .Where(m => m.MovementType == 'B')
                        .Sum(m => m.Price * m.Quantity),
                    Holdings = g.movements
                        .Sum(m => m.MovementType == 'B' ? m.Quantity : -m.Quantity)
                })
                .ToListAsync();

            return instrumentDtos;
        }

        public async Task<InstrumentReadDto?> GetInstrumentBalanceAsync(Guid instrumentId)
        {
            var instrumentDto = await _context.Instruments
                .Include(i => i.Movements)
                .Where(i => i.Id == instrumentId)
                .Select(i => new InstrumentReadDto
                {
                    Id = i.Id,
                    ExternalId = i.ExternalId,
                    Ticker = i.Ticker,
                    Description = i.Description,
                    InstrumentType = i.InstrumentType,
                    AveragePurchasePrice = i.Movements
                        .Where(m => m.MovementType == 'B')
                        .Select(m => m.Price).Average(),
                    InvestedAmount = i.Movements
                        .Where(m => m.MovementType == 'B')
                        .Sum(m => m.Price * m.Quantity),
                    Holdings = i.Movements
                        .Sum(m => m.MovementType == 'B' ? m.Quantity : -m.Quantity)
                })
                .FirstOrDefaultAsync();

            return instrumentDto;
        }
    }
}