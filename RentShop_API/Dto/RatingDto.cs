namespace RentShop_API.Dto;

public class RatingDto
{
    public Guid Id { get; set; }
    public int Grand { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}