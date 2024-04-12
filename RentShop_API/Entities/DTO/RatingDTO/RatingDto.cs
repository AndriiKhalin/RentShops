namespace Models.DTO.RatingDTO;

public class RatingDto
{
    public Guid Id { get; set; }

    public DateTime CreatedUpdatedAt { get; set; }
    public int Grand { get; set; }
    public string? Comment { get; set; }
}