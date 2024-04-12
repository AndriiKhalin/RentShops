using System.ComponentModel.DataAnnotations;

namespace Models.DTO.TransportAvailableDTO;

public class TransportAvailableForCreateDto
{
    [Required(ErrorMessage = "Count of transport is required")]
    public int CountTransport { get; set; }
}