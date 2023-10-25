namespace RentShop_API.Models.Entities;

public class LogTransaction
{
    public Guid Id { get; set; }
    public int Results { get; set; }
    public Guid TransactionId { get; set; }

    public Transaction? Transaction { get; set; }
}