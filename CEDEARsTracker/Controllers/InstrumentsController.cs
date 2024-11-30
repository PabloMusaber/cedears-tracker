using CEDEARsTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CEDEARsTracker.Controllers
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

        [HttpGet()]
        public async Task<ActionResult> CalculateAveragePurchasePrice()
        {
            Console.WriteLine($"--> Hit CalculateAveragePurchasePrice");

            try
            {
                await _instrumentService.CalculateAveragePurchasePriceAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching balance: {ex.Message}");
            }
        }

    }
}