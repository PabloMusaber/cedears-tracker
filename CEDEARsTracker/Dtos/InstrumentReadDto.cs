using static CEDEARsTracker.Enumerations.Enumerations;

namespace CEDEARsTracker.Dtos
{
    public class InstrumentReadDto
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AveragePurchasePrice { get; set; }
        public int Holdings { get; set; }
        public decimal InvestedAmount { get; set; }
        public InstrumentType InstrumentType { get; set; }
    }
}