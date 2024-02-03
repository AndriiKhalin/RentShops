namespace Entities.Models;

public class Category
{
    public Guid Id { get; set; }

    public string Name_Categories { get; set; }

    public List<Transport> Transports { get; set; }
}