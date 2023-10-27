namespace RentShop_API.Models.Entities;

public class Shop
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public TimeSpan WorkTimeStart { get; set; }
    public TimeSpan WorkTimeEnd { get; set; }

    public List<Order> Orders { get; set; } = new();
    public TransportAvailable? TransportAvailable { get; set; }
}