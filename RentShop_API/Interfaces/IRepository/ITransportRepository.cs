using Entities.Models;

namespace Interfaces.IRepository;

public interface ITransportRepository
{
    Task<IEnumerable<Transport>> GetTransports();

    Task<Transport> GetTransport(Guid id);


    Task<Category> GetCategoryByTransport(Guid transportId);

    Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);

    void DeleteTransport(Guid id);

    void UpdateTransport(Transport transport);

    Task CreateTransport(Guid categoryId, Transport transport);
}