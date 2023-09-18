using System.ComponentModel.DataAnnotations.Schema;

namespace RentShop_API.Models.Domain.Products;

public class Scooters
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public double Speed { get; set; }

    public double MaxWeight { get; set; }
}