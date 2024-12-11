using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static InstrumentService.Enumerations.Enumerations;

namespace InstrumentService.Models
{
    public class Instrument
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;
        public string Ticker { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public InstrumentType InstrumentType { get; set; }
    }
}
