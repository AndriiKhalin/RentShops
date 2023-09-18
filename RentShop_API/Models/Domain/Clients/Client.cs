namespace RentShop_API.Models.Domain.Clients;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? SurName { get; set; }

    public int StatusId { get; set; }
    public Status? Status { get; set; }

}