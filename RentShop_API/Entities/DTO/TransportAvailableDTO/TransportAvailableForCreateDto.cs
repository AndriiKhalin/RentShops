using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.TransportAvailableDTO;

public class TransportAvailableForCreateDto
{
    [Required(ErrorMessage = "Count of transport is required")]
    public int CountTransport { get; set; }
}