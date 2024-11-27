using System.ComponentModel.DataAnnotations;

namespace CEDEARsTracker.Dtos
{
    public class MovementCreateDto
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}