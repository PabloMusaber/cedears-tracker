using Microsoft.AspNetCore.Mvc;
using MarketService.Dtos;
using MarketService.Services.Interfaces;

namespace MarketService.Controllers
{
    [Route("api/mk/[controller]")]
    [ApiController]
    public class InstrumentsBalanceController : ControllerBase
    {
        private readonly IInstrumentBalanceService _instrumentBalanceService;


        public InstrumentsBalanceController(IInstrumentBalanceService instrumentBalanceService)
        {
            _instrumentBalanceService = instrumentBalanceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstrumentBalanceReadDto>> GetInstruments()
        {
            Console.WriteLine("--> Getting Instruments Balance...");

            var instruments = _instrumentBalanceService.GetAllInstrumentsBalance();

            return Ok(instruments);
        }
    }
}