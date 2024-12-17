using static MovementService.Enumerations.Enumerations;

namespace MovementService.Dtos
{
    public class InstrumentBalancePublishedDto
    {
        public Guid ExternalId { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Holdings { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal InvestedAmount { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public string Event { get; set; } = string.Empty;
    }
}