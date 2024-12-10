using InstrumentService.Dtos;
using InstrumentService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;


        public InstrumentsController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HttpPost]
        public async Task<ActionResult<InstrumentReadDto>> CreatePlatform(InstrumentCreateDto instrumentCreateDto)
        {
            Console.WriteLine("--> Creating instrument...");

            var platformReadDto = await _instrumentService.CreateInstrument(instrumentCreateDto);

            return Ok(platformReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstrumentReadDto>> GetInstruments()
        {
            Console.WriteLine("--> Getting Instruments...");

            var instruments = _instrumentService.GetAllInstruments();

            return Ok(instruments);
        }

    }
}