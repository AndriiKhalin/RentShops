namespace Entities.DTO.OrderDTO;

public class OrderForCreateDto
{
    public Guid Id { get; set; }

    public float Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }
}