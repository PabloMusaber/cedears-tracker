using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MarketService.Enumerations.Enumerations;

namespace MarketService.Models
{
    public class InstrumentBalance
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ExternalId { get; set; }

        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public int Holdings { get; set; }

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal AveragePurchasePrice { get; set; }

        public decimal InvestedAmount { get; set; }

        public InstrumentType InstrumentType { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;
    }
}