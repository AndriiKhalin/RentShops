using Entities.Models;

namespace Entities.DTO.CategoryDTO;

public class CategoryForCreateDto
{
    public Guid Id { get; set; }

    public string Name_Categories { get; set; }

    public List<Transport>? Transports { get; set; }
}