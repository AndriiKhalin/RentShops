using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Order
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number")]
    public float Price { get; set; }

    [Required(ErrorMessage = "Created at date is required")]

    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Date from is required")]
    public DateTime DateFrom { get; set; }

    [Required(ErrorMessage = "Date to is required")]
    public DateTime DateTo { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? ShopId { get; set; }

    public Shop? Shop { get; set; }

    public Guid? TransportId { get; set; }

    public Transport? Transport { get; set; }

    public Transaction? Transaction { get; set; }
}