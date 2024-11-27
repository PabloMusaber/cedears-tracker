using CEDEARsTracker.Dtos;
using CEDEARsTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CEDEARsTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public MovementsController(IMovementService movementService)
        {
            _movementService = movementService;
        }


        [HttpPost("{instrumentId}")]
        public async Task<ActionResult<MovementReadDto>> CreateMovementForInstrument(Guid instrumentId, MovementCreateDto movementCreateDto)
        {
            Console.WriteLine($"--> Hit CreateMovementForInstrument: {instrumentId}. Quantity: {movementCreateDto.Quantity}, Price: {movementCreateDto.Price}");

            var movementReadDto = await _movementService.InsertMovementAsync(instrumentId, movementCreateDto);

            if (movementReadDto == null)
                return NotFound();

            return Ok(movementReadDto);
        }

        [HttpGet("{instrumentId}", Name = "GetMovementsForInstrument")]
        public async Task<ActionResult<IEnumerable<MovementReadDto>>> GetMovementsForInstrument(Guid instrumentId)
        {
            Console.WriteLine($"--> Hit GetMovementsForInstrument: {instrumentId}");

            var movementReadDto = await _movementService.GetByInstrumentAsync(instrumentId);

            if (movementReadDto == null)
                return NotFound();

            return Ok(movementReadDto);
        }

        [HttpDelete("{movementId}")]
        public async Task<ActionResult> DeleteMovement(Guid movementId)
        {
            Console.WriteLine($"--> Hit DeleteMovement: {movementId}");

            if (await _movementService.DeleteAsync(movementId))
                return Ok();

            return NotFound();
        }

    }
}