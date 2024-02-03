namespace Entities.Models;

public class Order
{
    public Guid Id { get; set; }

    public float Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? ShopId { get; set; }

    public Shop? Shop { get; set; }

    public Guid? TransportId { get; set; }

    public Transport? Transport { get; set; }

    public Transaction? Transaction { get; set; }
}