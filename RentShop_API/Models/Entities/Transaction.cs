namespace RentShop_API.Models.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public float Sum { get; set; }
    public DateTime Date { get; set; }
    public Guid OrderId { get; set; }

    public Order? Order { get; set; }

    public LogTransaction? LogTransaction { get; set; }

}