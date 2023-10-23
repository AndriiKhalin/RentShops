namespace RentShop_API.Models.Entities;

public class Shop
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public DateTime ScheduleWork { get; set; }

    public List<Order> Orders { get; set; } = new();
}