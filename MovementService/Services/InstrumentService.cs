using AutoMapper;
using MovementService.Dtos;
using MovementService.Infraestructure.Repositories.Interfaces;
using MovementService.Models;
using MovementService.Services.Interfaces;

namespace MovementService.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMapper _mapper;

        public InstrumentService(IInstrumentRepository instrumentRepository, IMapper mapper)
        {
            _instrumentRepository = instrumentRepository;
            _mapper = mapper;
        }

        public async Task<InstrumentReadDto> CreateInstrument(InstrumentCreateDto instrumentCreateDto)
        {
            if (instrumentCreateDto == null)
            {
                throw new ArgumentNullException(nameof(instrumentCreateDto));
            }

            var instrumentModel = _mapper.Map<Instrument>(instrumentCreateDto);

            await _instrumentRepository.CreateInstrumentAsync(instrumentModel);

            return _mapper.Map<InstrumentReadDto>(instrumentModel);
        }

        public IEnumerable<InstrumentReadDto> GetAllInstruments()
        {
            var instruments = _instrumentRepository.GetAllInstruments();
            return _mapper.Map<IEnumerable<InstrumentReadDto>>(instruments);
        }

        public bool InstrumentExists(Guid instrumentId)
        {
            return _instrumentRepository.InstrumentExists(instrumentId);
        }

    }
}