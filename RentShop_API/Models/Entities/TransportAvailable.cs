namespace RentShop_API.Models.Entities;

public class TransportAvailable
{
    public Guid Id { get; set; }
    public Guid CountTraansport {  get; set; }  
    public Guid TransportId { get; set; }
    public Transport? Transport { get; set; }
    public Guid ShopId { get; set; }
    public Shop? Shop { get; set; }
}