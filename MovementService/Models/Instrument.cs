using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.Models
{
    public class Instrument : BaseEntity
    {
        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        public Guid ExternalId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal AveragePurchasePrice { get; set; }

        public InstrumentType InstrumentType { get; set; }

        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}
