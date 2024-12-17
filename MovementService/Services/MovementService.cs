using AutoMapper;
using MovementService.AsyncDataServices;
using MovementService.Dtos;
using MovementService.Infraestructure.Repositories.Interfaces;
using MovementService.Models;
using MovementService.Services.Interfaces;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;
        private readonly IInstrumentService _instrumentService;
        private readonly IMessageBusClient _messageBusClient;

        public MovementService(IMovementRepository movementRepository, IMapper mapper, IInstrumentService instrumentService, IMessageBusClient messageBusClient)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
            _instrumentService = instrumentService;
            _messageBusClient = messageBusClient;
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
            //await _instrumentService.CalculateAveragePurchasePriceAsync(instrumentId);

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

        public async Task PublishNewInstrumentBalanceAsync(Guid instrumentId)
        {
            var instrumentReadDto = await _instrumentService.GetInstrumentBalanceAsync(instrumentId);
            var instrumentBalancePublishedDto = _mapper.Map<InstrumentBalancePublishedDto>(instrumentReadDto);
            instrumentBalancePublishedDto.Event = "Instrument_Balance_Published";
            _messageBusClient.PublishNewInstrumentBalance(instrumentBalancePublishedDto);
        }
    }
}