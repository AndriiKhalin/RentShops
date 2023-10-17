using System.ComponentModel.DataAnnotations.Schema;
using RentShop_API.Models.Domain.Clients;
using RentShop_API.Models.Domain.Products;

namespace RentShop_API.Models.Domain.Orders;

public class Order
{
    public int Id { get; set; }

    public long OrderNumber { get; set; }

    public DateTime Date { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    public int ClientId { get; set; }

    public Client? Client { get; set; }

    public int VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

}