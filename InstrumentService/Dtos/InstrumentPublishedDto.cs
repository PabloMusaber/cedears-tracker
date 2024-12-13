using static InstrumentService.Enumerations.Enumerations;

namespace InstrumentService.Dtos
{
    public class InstrumentPublishedDto
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public InstrumentType InstrumentType { get; set; }
        public string Event { get; set; } = string.Empty;
    }
}