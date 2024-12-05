using AutoMapper;
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Models;
using CEDEARsTracker.Services.Interfaces;
using static CEDEARsTracker.Enumerations.Enumerations;

namespace CEDEARsTracker.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;
        private readonly IInstrumentService _instrumentService;

        public MovementService(IMovementRepository movementRepository, IMapper mapper, IInstrumentService instrumentService)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
            _instrumentService = instrumentService;
        }

        public async Task<MovementReadDto?> InsertMovementAsync(Guid instrumentId, MovementCreateDto movementCreateDto)
        {
            if (movementCreateDto == null)
            {
                throw new ArgumentNullException(nameof(movementCreateDto));
            }

            if (!IsValidMovementType(movementCreateDto.MovementType))
            {
                throw new ArgumentException($"Invalid MovementType value: {movementCreateDto.MovementType}");
            }

            if (!_instrumentService.InstrumentExists(instrumentId))
            {
                return null;
            }

            var movement = _mapper.Map<Movement>(movementCreateDto);
            movement.InstrumentId = instrumentId;

            await _movementRepository.InsertMovementAsync(movement);
            await _instrumentService.CalculateAveragePurchasePriceAsync(instrumentId);

            return _mapper.Map<MovementReadDto>(movement);
        }

        public async Task<IEnumerable<MovementReadDto>?> GetByInstrumentAsync(Guid instrumentId)
        {
            if (!_instrumentService.InstrumentExists(instrumentId))
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

        private bool IsValidMovementType(char movementTypeChar)
        {
            return Enum.IsDefined(typeof(MovementType), (int)movementTypeChar);
        }

    }
}