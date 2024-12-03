using static CEDEARsTracker.Enumerations.Enumerations;

namespace CEDEARsTracker.Dtos
{
    public class InvestmentsReturnsDto
    {
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Holdings { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal InvestmentsReturns { get; set; }
        public decimal ProfitLoss { get; set; }
        public InstrumentType InstrumentType { get; set; }
    }
}