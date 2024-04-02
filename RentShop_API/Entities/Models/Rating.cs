using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Rating
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Created at date is required")]
    public DateTime CreatedUpdatedAt { get; set; }

    [Required(ErrorMessage = "Grand is required")]
    [Range(0, 5, ErrorMessage = "Grand must be from 0 to 5")]
    public int Grand { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters")]
    public string? Comment { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? TransportId { get; set; }
    public Transport? Transport { get; set; }
}