using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Transaction
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Sum is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Sum must be a positive number")]
    public float Sum { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
    public Guid? OrderId { get; set; }

    public Order? Order { get; set; }

    public LogTransaction? LogTransaction { get; set; }
}