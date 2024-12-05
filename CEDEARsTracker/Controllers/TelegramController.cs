
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Models;
using CEDEARsTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CEDEARsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramBotService _telegramBotService;

        public TelegramController(ITelegramBotService telegramBotService)
        {
            _telegramBotService = telegramBotService;
        }

        [HttpGet()]
        public async Task<ActionResult> SendInvestmentsReturnsSpreadsheet()
        {
            try
            {
                await _telegramBotService.SendInvestmentsReturnsSpreadsheet();
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching balance: {ex.Message}");
            }
        }

    }
}