using System.ComponentModel.DataAnnotations;

namespace Models.DTO.LogTransactionDTO;

public class LogTransactionForCreateDto
{
    [Required(ErrorMessage = "Results is required")]
    public bool Results { get; set; }
}