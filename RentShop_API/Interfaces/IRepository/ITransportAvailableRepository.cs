using Models.DTO.TransportAvailableDTO;
using Models.Entities;

namespace Interfaces.IRepository;

public interface ITransportAvailableRepository
{
    Task<IEnumerable<TransportAvailable>> GetTransportAvailables();

    Task<TransportAvailable> GetTransportAvailable(Guid id);

    Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId);
    Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId);

    Task<bool> TransportAvailableExists(Guid id);

    Task<TransportAvailable> CreateTransportAvailable(Guid transportId, Guid shopId, TransportAvailableForCreateDto transportAvailable);

    void DeleteTransportAvailable(Guid id);

    Task UpdateTransportAvailable(Guid transportAvailableId, TransportAvailableForUpdateDto transportAvailable);
}