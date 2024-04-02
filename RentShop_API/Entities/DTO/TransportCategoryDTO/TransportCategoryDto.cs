using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.CategoryDTO;

public class TransportCategoryDto
{
    public Guid Id { get; set; }
    public DateTime CreatedUpdatedAt { get; set; }

    public string Name_Categories { get; set; }

}