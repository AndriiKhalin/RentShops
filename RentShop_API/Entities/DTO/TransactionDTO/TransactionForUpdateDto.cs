using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.TransactionDTO;

public class TransactionForUpdateDto
{
    [Required(ErrorMessage = "Sum is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Sum must be a positive number")]
    public float Sum { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
}
