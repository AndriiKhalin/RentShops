using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.TransportAvailableDTO;

public class TransportAvailableForUpdateDto
{
    [Required(ErrorMessage = "Count of transport is required")]
    public int CountTransport { get; set; }
}