using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class TransportCategory
{
    public Guid Id { get; set; }


    [Required(ErrorMessage = "Created at date is required")]
    public DateTime CreatedUpdatedAt { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string Name_Categories { get; set; }

    public List<Transport> Transports { get; set; }
}