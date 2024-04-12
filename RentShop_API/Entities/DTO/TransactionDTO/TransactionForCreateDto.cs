using System.ComponentModel.DataAnnotations;

namespace Models.DTO.TransactionDTO;

public class TransactionForCreateDto
{
    [Required(ErrorMessage = "Sum is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Sum must be a positive number")]
    public float Sum { get; set; }

}