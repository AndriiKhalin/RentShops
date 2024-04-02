using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.ShopDTO;

public class ShopDto
{
    public Guid Id { get; set; }

    public DateTime CreatedUpdatedAt { get; set; }
    public string Address { get; set; }

    public TimeSpan WorkTimeStart { get; set; }
    public TimeSpan WorkTimeEnd { get; set; }

    public string? ImgUrl { get; set; }
}