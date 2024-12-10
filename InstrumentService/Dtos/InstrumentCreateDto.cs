using System.ComponentModel.DataAnnotations;
using static InstrumentService.Enumerations.Enumerations;

namespace InstrumentService.Dtos
{
    public class InstrumentCreateDto
    {
        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public InstrumentType InstrumentType { get; set; }
    }
}