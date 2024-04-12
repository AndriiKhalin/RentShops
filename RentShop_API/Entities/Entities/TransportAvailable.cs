using System.ComponentModel.DataAnnotations;

namespace Models.Entities;

public class TransportAvailable
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Created at date is required")]
    public DateTime CreatedUpdatedAt { get; set; }

    [Required(ErrorMessage = "Count of transport is required")]
    public int CountTransport { get; set; }
    public Guid? TransportId { get; set; }
    public Transport? Transport { get; set; }
    public Guid? ShopId { get; set; }
    public Shop? Shop { get; set; }
}