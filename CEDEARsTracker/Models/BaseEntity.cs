namespace CEDEARsTracker.Models;

using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity
{
    [Required]
    public Guid Id { get; set; }

    public DateTime InsertDate { get; set; } = DateTime.Now;
}