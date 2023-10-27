namespace RentShop_API.Models.Entities;

public class Order
{
    public Guid Id { get; set; }

    public float Price { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public List<Shop> Shops { get; set; } = new();

    public List<Transport> Transports { get; set; } = new();

    public Transaction? Transaction { get; set; }
}