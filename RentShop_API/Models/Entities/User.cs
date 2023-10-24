namespace RentShop_API.Models.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }

    public string Phone { get; set; }

    public string Status { get; set; }

    public List<Order> Orders { get; set; } = new();

    public List<Rating> Ratings { get; set; } = new();
}