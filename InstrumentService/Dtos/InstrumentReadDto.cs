using static InstrumentService.Enumerations.Enumerations;

namespace InstrumentService.Dtos
{
    public class InstrumentReadDto
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public InstrumentType InstrumentType { get; set; }
    }
}