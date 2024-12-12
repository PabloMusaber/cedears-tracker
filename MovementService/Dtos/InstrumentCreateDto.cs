using System.ComponentModel.DataAnnotations;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.Dtos
{
    public class InstrumentCreateDto
    {
        [Required]
        public Guid ExternalId { get; set; }

        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public InstrumentType InstrumentType { get; set; }
    }
}