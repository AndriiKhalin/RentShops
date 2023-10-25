namespace RentShop_API.Models.Entities;

public class Shop
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public TimeOnly WorkTimeStart { get; set; }
    public TimeOnly WorkTimeEnd { get; set; }

    public List<Order> Orders { get; set; } = new();
    public TransportAvailable? TransportAvailable { get; set; }
}