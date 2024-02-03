namespace Entities.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }

    public string Phone { get; set; }

    public string Role { get; set; }

    public List<Order> Orders { get; set; } = new();

    public List<Rating> Ratings { get; set; } = new();
}