using AutoMapper;
using InstrumentService.Controllers;
using InstrumentService.Dtos;
using InstrumentService.Infraestructure.Repositories.Interfaces;
using InstrumentService.Models;
using InstrumentService.Services.Interfaces;

namespace InstrumentService.Services
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