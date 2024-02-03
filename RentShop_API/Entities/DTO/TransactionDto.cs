namespace Entities.DTO;

public class TransactionDto
{
    public Guid Id { get; set; }
    public float Sum { get; set; }
    public DateTime Date { get; set; }
}