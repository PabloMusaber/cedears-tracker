using System.ComponentModel.DataAnnotations.Schema;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.Models
{
    public class Instrument : BaseEntity
    {
        public string Ticker { get; set; } = string.Empty;

        // [Required]
        // public int ExternalId { get; set; }

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal AveragePurchasePrice { get; set; }

        public InstrumentType InstrumentType { get; set; }

        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}
