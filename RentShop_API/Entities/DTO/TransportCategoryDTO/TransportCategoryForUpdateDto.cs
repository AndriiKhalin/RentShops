using System.ComponentModel.DataAnnotations;

namespace Models.DTO.TransportCategoryDTO;

public class TransportCategoryForUpdateDto
{
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string? Name_Categories { get; set; }

}