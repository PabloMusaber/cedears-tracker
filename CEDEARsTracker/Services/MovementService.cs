using AutoMapper;
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Models;

namespace CEDEARsTracker.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMapper _mapper;

        public MovementService(IMovementRepository movementRepository, IInstrumentRepository instrumentRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _instrumentRepository = instrumentRepository;
            _mapper = mapper;
        }

        public async Task<MovementReadDto?> InsertMovementAsync(Guid instrumentId, MovementCreateDto movementCreateDto)
        {
            if (movementCreateDto == null)
            {
                throw new ArgumentNullException(nameof(movementCreateDto));
            }

            if (!_instrumentRepository.InstrumentExists(instrumentId))
            {
                return null;
            }

            var movement = _mapper.Map<Movement>(movementCreateDto);
            movement.InstrumentId = instrumentId;

            await _movementRepository.InsertMovementAsync(movement);

            return _mapper.Map<MovementReadDto>(movement);
        }

        public async Task<IEnumerable<MovementReadDto>?> GetByInstrumentAsync(Guid instrumentId)
        {
            if (!_instrumentRepository.InstrumentExists(instrumentId))
            {
                return null;
            }

            var movements = await _movementRepository.GetByInstrumentAsync(instrumentId);
            return _mapper.Map<IEnumerable<MovementReadDto>>(movements);
        }

        public async Task<bool> DeleteAsync(Guid movementId)
        {
            return await _movementRepository.DeleteAsync(movementId);
        }
    }
}