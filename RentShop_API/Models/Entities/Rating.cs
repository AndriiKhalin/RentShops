namespace RentShop_API.Models.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public int Grand { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? TransportsId { get; set; }
    public Transport? Transport { get; set; }


}