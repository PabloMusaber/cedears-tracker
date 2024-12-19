using Microsoft.AspNetCore.Mvc;
using SpreadsheetService.AsyncDataServices;
using SpreadsheetService.Dtos;

namespace CEDEARsTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpreadsheetsController : ControllerBase
    {
        private readonly IMessageBusClient _messageBusClient;

        public SpreadsheetsController(IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient;
        }

        [HttpGet()]
        public ActionResult SendInvestmentsReturnsSpreadsheet()
        {
            try
            {
                var request = new RequestInstrumentBalanceExportDto();
                request.Event = "Request_Instrument_Balance_Export";
                _messageBusClient.RequestInstrumentBalanceExport(request);
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching balance: {ex.Message}");
            }
        }

    }
}