using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class LogTransaction
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Results is required")]
    public bool Results { get; set; }
    public Guid? TransactionId { get; set; }

    public Transaction? Transaction { get; set; }
}