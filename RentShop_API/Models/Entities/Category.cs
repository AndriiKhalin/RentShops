namespace RentShop_API.Models.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name_Category { get; set; }

    public List<Transport> Transports { get; set; }
}