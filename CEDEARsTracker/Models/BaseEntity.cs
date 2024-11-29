using System.ComponentModel.DataAnnotations;

namespace CEDEARsTracker.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;
    }
}