using AutoMapper;
using InstrumentService.AsyncDataServices;
using InstrumentService.Dtos;
using InstrumentService.Services.Interfaces;
using InstrumentService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IHttpMovementDataClient _movementDataClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;

        public InstrumentsController(IInstrumentService instrumentService, IHttpMovementDataClient movementDataClient,
            IMessageBusClient messageBusClient, IMapper mapper)
        {
            _instrumentService = instrumentService;
            _movementDataClient = movementDataClient;
            _messageBusClient = messageBusClient;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<InstrumentReadDto>> CreateInstrument(InstrumentCreateDto instrumentCreateDto)
        {
            Console.WriteLine("--> Creating instrument...");

            var instrumentReadDto = await _instrumentService.CreateInstrument(instrumentCreateDto);

            // Send Sync Message to Movement Service
            try
            {
                await _movementDataClient.SendInstrumentToMovementService(instrumentReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            // Send Async Message
            try
            {
                var instrumentPublishedDto = _mapper.Map<InstrumentPublishedDto>(instrumentReadDto);
                instrumentPublishedDto.Event = "Instrument_Published";
                _messageBusClient.PublishNewInstrument(instrumentPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return Ok(instrumentReadDto);
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