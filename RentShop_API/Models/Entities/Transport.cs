namespace RentShop_API.Models.Entities;

public class Transport
{
    public Guid Id { get; set; }

    public string Model { get; set; }

    public string Mark { get; set; }

    public float PriceMinute { get; set; }

    public int MaxSpeed { get; set; }

    public string ImgUrl { get; set; }

    public int MaxWeight { get; set; }

    public Guid? CategoryId { get; set; }

    public Category? Category { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<Rating> Ratings { get; set; } = new();
    public TransportAvailable? TransportAvailable { get; set; }

}