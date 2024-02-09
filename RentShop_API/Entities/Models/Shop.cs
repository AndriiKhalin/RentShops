using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Shop
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Work time start is required")]
    public TimeSpan WorkTimeStart { get; set; }

    [Required(ErrorMessage = "Work time end is required")]
    public TimeSpan WorkTimeEnd { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<TransportAvailable> TransportAvailables { get; set; } = new();
}