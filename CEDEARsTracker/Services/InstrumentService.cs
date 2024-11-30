using CEDEARsTracker.Infraestructure.Repositories;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Services.Interfaces;

namespace CEDEARsTracker.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;

        public InstrumentService(IInstrumentRepository instrumentRepository)
        {
            _instrumentRepository = instrumentRepository;
        }

        public async Task CalculateAveragePurchasePriceAsync()
        {
            var averagePurchasePrices = await _instrumentRepository.GetAveragePurchasePriceAsync();

            foreach (var item in averagePurchasePrices)
            {
                await _instrumentRepository.UpdateAveragePurchasePriceAsync(item.InstrumentId, item.AveragePurchasePrice);
            }
        }
    }
}