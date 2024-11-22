using System.ComponentModel.DataAnnotations.Schema;
using static CEDEARsTracker.Enumerations.Enumerations;

namespace CEDEARsTracker.Models
{
    public class Instrument : BaseEntity
    {
        public string Ticker { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "DECIMAL(18, 2)")]
        public decimal AveragePurchasePrice { get; set; }

        public InstrumentType InstrumentType { get; set; }

        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}
