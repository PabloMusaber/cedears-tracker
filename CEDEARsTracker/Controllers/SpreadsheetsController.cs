using CEDEARsTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CEDEARsTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpreadsheetsController : ControllerBase
    {
        private readonly ISpreadsheetService _spreadsheetService;

        public SpreadsheetsController(ISpreadsheetService spreadsheetService)
        {
            _spreadsheetService = spreadsheetService;
        }

        [HttpGet("investment-returns")]
        public async Task<ActionResult> InvestmentsReturnsSpreadsheet()
        {
            Console.WriteLine($"--> Hit InvestmentsReturnsSpreadsheet");

            try
            {
                var spreadsheetBytes = await _spreadsheetService.InvestmentsReturnsSpreadsheet();

                return File(
                    spreadsheetBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Investment_Returns_{DateTime.Today.ToShortDateString()}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}