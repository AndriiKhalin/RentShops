using Entities.Models;

namespace Interfaces.IRepository;

public interface ITransportRepository
{
    Task<IEnumerable<Transport>> GetTransports();

    Task<Transport> GetTransport(Guid id);

    Task<Transport> GetTransportByOrder(Guid orderId);

    Task<List<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);

    void DeleteTransport(Guid id);

    void UpdateTransport(Transport transport);

    Task CreateTransport(Transport transport);
}