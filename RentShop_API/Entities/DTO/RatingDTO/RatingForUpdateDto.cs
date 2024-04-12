using System.ComponentModel.DataAnnotations;

namespace Models.DTO.RatingDTO;

public class RatingForUpdateDto
{
    [Range(0, 5, ErrorMessage = "Grand must be from 0 to 5")]
    public int? Grand { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters")]
    public string? Comment { get; set; }

}