using Entities.Models;

namespace Interfaces.IRepository;

public interface ITransportAvailableRepository
{
    Task<IEnumerable<TransportAvailable>> GetTransportAvailables();

    Task<TransportAvailable> GetTransportAvailable(Guid id);

    Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId);
    Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId);

    Task<bool> TransportAvailableExists(Guid id);

    Task CreateTransportAvailable(TransportAvailable transportAvailable);

    void DeleteTransportAvailable(Guid id);

    void UpdateTransportAvailable(TransportAvailable transportAvailable);
}