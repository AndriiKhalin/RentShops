using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.LogTransactionDTO;

public class LogTransactionForCreateDto
{
    [Required(ErrorMessage = "Results is required")]
    public bool Results { get; set; }
}