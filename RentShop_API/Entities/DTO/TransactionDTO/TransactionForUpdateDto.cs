using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.TransactionDTO;

public class TransactionForUpdateDto
{
    [Range(0, float.MaxValue, ErrorMessage = "Sum must be a positive number")]
    public float? Sum { get; set; }

}
