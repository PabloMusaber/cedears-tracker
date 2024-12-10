using System.ComponentModel.DataAnnotations;

namespace MovementService.Dtos
{
    public class MovementCreateDto
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public char MovementType { get; set; } = (char)Enumerations.Enumerations.MovementType.Buy;
    }
}