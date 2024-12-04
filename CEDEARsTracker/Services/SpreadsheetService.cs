using AutoMapper;
using CEDEARsTracker.Dtos;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Services.Interfaces;
using ClosedXML.Excel;

namespace CEDEARsTracker.Services
{
    public class SpreadsheetService : ISpreadsheetService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMapper _mapper;
        private readonly IMarketClientService _marketClientService;

        public SpreadsheetService(IInstrumentRepository instrumentRepository, IMapper mapper, IMarketClientService marketClientService)
        {
            _instrumentRepository = instrumentRepository;
            _mapper = mapper;
            _marketClientService = marketClientService;
        }

        public async Task<byte[]> InvestmentsReturnsSpreadsheet()
        {
            var instrumentsReadDto = await _instrumentRepository.GetAllInstrumentsAsync();
            var investmentsReturnsDto = _mapper.Map<List<InvestmentsReturnsDto>>(instrumentsReadDto);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Investments_Returns");

                worksheet.Cell(1, 1).Value = "Description";
                worksheet.Cell(1, 2).Value = "Ticker";
                worksheet.Cell(1, 3).Value = "Holdings";
                worksheet.Cell(1, 4).Value = "AveragePurchasePrice";
                worksheet.Cell(1, 5).Value = "Invested";
                worksheet.Cell(1, 6).Value = "Current Amount";
                worksheet.Cell(1, 7).Value = "Profit";

                var row = 2;
                decimal totalInvested = 0;
                decimal totalCurrentAmount = 0;
                foreach (var instrument in investmentsReturnsDto)
                {
                    var currentPrice = await _marketClientService.GetCurrentPrice(instrument.Ticker, instrument.InstrumentType.ToString(), "A-24HS");

                    worksheet.Cell(row, 1).Value = instrument.Description;
                    worksheet.Cell(row, 2).Value = instrument.Ticker;
                    worksheet.Cell(row, 3).Value = instrument.Holdings;
                    worksheet.Cell(row, 4).Value = instrument.AveragePurchasePrice;
                    worksheet.Cell(row, 5).Value = instrument.InvestedAmount;
                    worksheet.Cell(row, 6).Value = instrument.Holdings * currentPrice.Price;
                    worksheet.Cell(row, 7).Value = ((currentPrice.Price - instrument.AveragePurchasePrice) / instrument.AveragePurchasePrice) * 100; // ROI
                    row++;
                    totalInvested += instrument.InvestedAmount;
                    totalCurrentAmount += instrument.Holdings * currentPrice.Price;
                }

                worksheet.Cell(row + 3, 1).Value = "Total Invested";
                worksheet.Cell(row + 3, 2).Value = totalInvested;

                worksheet.Cell(row + 4, 1).Value = "Current Amount";
                worksheet.Cell(row + 4, 2).Value = totalCurrentAmount;

                if (totalCurrentAmount > totalInvested)
                {
                    worksheet.Cell(row + 4, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else
                {
                    worksheet.Cell(row + 4, 2).Style.Fill.BackgroundColor = XLColor.CoralRed;
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}