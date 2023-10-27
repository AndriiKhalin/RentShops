namespace RentShop_API.Models.Entities;

public class LogTransaction
{
    public Guid Id { get; set; }
    public bool Results { get; set; }
    public Guid? TransactionId { get; set; }

    public Transaction? Transaction { get; set; }
}