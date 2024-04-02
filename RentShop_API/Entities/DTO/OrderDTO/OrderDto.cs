using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.OrderDTO;

public class OrderDto
{
    public Guid Id { get; set; }

    public float Price { get; set; }

    public DateTime CreatedUpdatedAt { get; set; }

    public DateTime OrderDateFrom { get; set; }

    public DateTime OrderDateTo { get; set; }

    public string TransportImgUrl { get; set; }
}