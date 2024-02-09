namespace Entities.DTO.TransportDTO;

public class TransportDto
{
    public Guid Id { get; set; }

    public string Model { get; set; }

    public string Mark { get; set; }

    public float PriceMinute { get; set; }

    public int MaxSpeed { get; set; }

    public string ImgUrl { get; set; }

    public int MaxWeight { get; set; }

    public Guid? CategoryId { get; set; }
}