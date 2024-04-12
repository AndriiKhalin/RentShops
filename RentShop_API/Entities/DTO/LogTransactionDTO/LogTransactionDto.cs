namespace Models.DTO.LogTransactionDTO;

public class LogTransactionDto
{
    public Guid Id { get; set; }
    public DateTime CreatedUpdatedAt { get; set; }
    public bool Results { get; set; }
}