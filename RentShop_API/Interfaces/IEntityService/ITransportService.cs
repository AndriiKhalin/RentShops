using Models.DTO.TransportDTO;
using Models.Entities;

namespace Interfaces.IEntityService;

public interface ITransportService
{
    Task<List<Transport>> GetTransports();

    Task<Transport?> GetTransport(Guid id);


    Task<TransportCategory?> GetCategoryByTransport(Guid transportId);

    Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);

    void DeleteTransport(Guid id);

    Task UpdateTransport(Guid transportId, TransportForUpdateDto transport);

    Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport);
}