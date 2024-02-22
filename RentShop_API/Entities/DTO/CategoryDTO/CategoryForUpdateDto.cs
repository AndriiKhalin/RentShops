using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.CategoryDTO;

public class CategoryForUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string Name_Categories { get; set; }

}