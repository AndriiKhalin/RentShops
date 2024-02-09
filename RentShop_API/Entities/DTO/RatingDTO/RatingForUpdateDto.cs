namespace Entities.DTO.RatingDTO;

public class RatingForUpdateDto
{
    public Guid Id { get; set; }
    public int Grand { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}