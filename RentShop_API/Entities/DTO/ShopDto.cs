namespace Entities.DTO;

public class ShopDto
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public TimeSpan WorkTimeStart { get; set; }
    public TimeSpan WorkTimeEnd { get; set; }
}