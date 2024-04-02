using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.TransportAvailableDTO;

public class TransportAvailableDto
{
    public Guid Id { get; set; }
    public DateTime CreatedUpdatedAt { get; set; }
    public int CountTransport { get; set; }
}