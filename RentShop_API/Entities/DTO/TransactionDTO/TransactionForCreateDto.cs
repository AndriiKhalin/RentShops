namespace Entities.DTO.TransactionDTO;

public class TransactionForCreateDto
{
    public Guid Id { get; set; }
    public float Sum { get; set; }
    public DateTime Date { get; set; }
}