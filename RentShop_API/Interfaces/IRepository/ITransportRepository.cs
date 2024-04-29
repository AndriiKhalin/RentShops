using Models.DTO.TransportDTO;
using Models.Entities;

namespace Interfaces.IRepository;

public interface ITransportRepository
{
    Task<List<Transport>> GetTransports();

    Task<Transport?> GetTransport(Guid id);

    Task<TransportCategory?> GetCategoryByTransport(Guid transportId);

    Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);

    void DeleteTransport(Guid id);

    Task UpdateTransport(Transport transport);

    Task CreateTransport(Transport transport);
}