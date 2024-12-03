using AutoMapper;
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Services.Interfaces;

namespace CEDEARsTracker.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMapper _mapper;
        private readonly IMarketClientService _marketClientService;

        public InstrumentService(IInstrumentRepository instrumentRepository, IMapper mapper, IMarketClientService marketClientService)
        {
            _instrumentRepository = instrumentRepository;
            _mapper = mapper;
            _marketClientService = marketClientService;
        }

        public async Task CalculateAveragePurchasePriceAsync()
        {
            var averagePurchasePrices = await _instrumentRepository.GetAveragePurchasePriceAsync();

            foreach (var item in averagePurchasePrices)
            {
                await _instrumentRepository.UpdateAveragePurchasePriceAsync(item.InstrumentId, item.AveragePurchasePrice);
            }
        }

        public async Task<List<InvestmentsReturnsDto>> CalculateInvestmentsReturns()
        {
            var instrumentsReadDto = await _instrumentRepository.GetAllInstrumentsAsync();
            var investmentsReturnsDto = _mapper.Map<List<InvestmentsReturnsDto>>(instrumentsReadDto);

            foreach (var instrument in investmentsReturnsDto)
            {
                var currentPrice = await _marketClientService.GetCurrentPrice(instrument.Ticker, instrument.InstrumentType.ToString(), "A-24HS");
                instrument.CurrentPrice = currentPrice.Price;
                instrument.InvestmentsReturns = ((instrument.CurrentPrice - instrument.AveragePurchasePrice) / instrument.AveragePurchasePrice) * 100; // ROI
                instrument.ProfitLoss = instrument.Holdings * (instrument.CurrentPrice - instrument.AveragePurchasePrice);
            }

            return investmentsReturnsDto;
        }
    }
}