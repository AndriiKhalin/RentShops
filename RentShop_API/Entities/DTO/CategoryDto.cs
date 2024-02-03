using Entities.Models;

namespace Entities.DTO;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Name_Categories { get; set; }

    public List<Transport> Transports { get; set; }
}