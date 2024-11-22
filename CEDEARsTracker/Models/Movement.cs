using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDEARsTracker.Models
{
    public class Movement : BaseEntity
    {
        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public Guid InstrumentId { get; set; } // Foreign Key

        public Instrument? Instrument { get; set; } // Navegation property
    }
}