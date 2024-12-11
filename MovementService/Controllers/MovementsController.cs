using Microsoft.AspNetCore.Mvc;
using MovementService.Dtos;
using MovementService.Services.Interfaces;

namespace MovementService.Controllers
{
    [Route("api/m/instruments/{instrumentId}/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _movementService;


        public MovementsController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpPost]
        public async Task<ActionResult<MovementReadDto>> CreateMovementForInstrument(Guid instrumentId, MovementCreateDto movementCreateDto)
        {
            Console.WriteLine($"--> Hit CreateMovementForInstrument: {instrumentId}. Quantity: {movementCreateDto.Quantity}, Price: {movementCreateDto.Price}");

            var movementReadDto = await _movementService.InsertMovementAsync(instrumentId, movementCreateDto);

            if (movementReadDto == null)
                return NotFound();

            return Ok(movementReadDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovementReadDto>>> GetMovementsForInstrument(Guid instrumentId)
        {
            Console.WriteLine($"--> Hit GetMovementsForInstrument: {instrumentId}");

            var movementReadDto = await _movementService.GetByInstrumentAsync(instrumentId);

            if (movementReadDto == null)
                return NotFound();

            return Ok(movementReadDto);
        }

    }
}