using AutoMapper;
using MarketService.Dtos;
using MarketService.Infraestructure.Repositories.Interfaces;
using MarketService.Models;
using MarketService.Services.Interfaces;
using InstrumentBalance = MarketService.Models.InstrumentBalance;

namespace MarketService.Services
{
    public class InstrumentBalanceService : IInstrumentBalanceService
    {
        private readonly IInstrumentBalanceRepository _instrumentBalanceRepository;
        private readonly IMapper _mapper;

        public InstrumentBalanceService(IInstrumentBalanceRepository instrumentRepository, IMapper mapper)
        {
            _instrumentBalanceRepository = instrumentRepository;
            _mapper = mapper;
        }

        public async Task<InstrumentBalanceReadDto> CreateInstrumentBalance(InstrumentBalanceCreateDto instrumentBalanceCreateDto)
        {
            if (instrumentBalanceCreateDto == null)
            {
                throw new ArgumentNullException(nameof(instrumentBalanceCreateDto));
            }

            var instrumentModel = _mapper.Map<InstrumentBalance>(instrumentBalanceCreateDto);

            await _instrumentBalanceRepository.CreateInstrumentBalanceAsync(instrumentModel);

            return _mapper.Map<InstrumentBalanceReadDto>(instrumentModel);
        }

        public bool ExternalInstrumentExists(Guid externalInstrumentId)
        {
            return _instrumentBalanceRepository.ExternalInstrumentExists(externalInstrumentId);
        }

        public IEnumerable<InstrumentBalanceReadDto> GetAllInstrumentsBalance()
        {
            var instruments = _instrumentBalanceRepository.GetAllInstrumentsBalance();
            return _mapper.Map<IEnumerable<InstrumentBalanceReadDto>>(instruments);
        }

        public bool InstrumentBalanceExists(Guid instrumentBalanceId)
        {
            return _instrumentBalanceRepository.InstrumentBalanceExists(instrumentBalanceId);
        }

        public async Task UpdateInstrumentBalance(InstrumentBalanceCreateDto instrumentBalanceCreateDto)
        {
            var instrument = _mapper.Map<InstrumentBalance>(instrumentBalanceCreateDto);
            await _instrumentBalanceRepository.UpdateInstrumentBalanceASync(instrument);
        }
    }
}