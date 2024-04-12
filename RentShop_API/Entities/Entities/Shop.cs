using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class Shop
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Created at date is required")]
    public DateTime CreatedUpdatedAt { get; set; }


    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Work time start is required")]
    public TimeSpan WorkTimeStart { get; set; }

    [Required(ErrorMessage = "Work time end is required")]
    public TimeSpan WorkTimeEnd { get; set; }
    [Required(ErrorMessage = "Image URL is required")]
    [StringLength(100, ErrorMessage = "Image URL cannot be longer than 100 characters")]
    public string ImgUrl { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<TransportAvailable> TransportAvailables { get; set; } = new();
}