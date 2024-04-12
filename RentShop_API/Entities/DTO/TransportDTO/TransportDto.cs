namespace Models.DTO.TransportDTO;

public class TransportDto
{
    public Guid Id { get; set; }

    public DateTime CreatedUpdatedAt { get; set; }

    public string Mark { get; set; }

    public string Model { get; set; }

    public float PriceMinute { get; set; }

    public int MaxSpeed { get; set; }

    public string ImgUrl { get; set; }

    public int MaxWeight { get; set; }


}