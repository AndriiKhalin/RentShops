using Models.DTO.TransportDTO;
using Models.Entities;

namespace Services.Interfaces.IRepository;

public interface ITransportRepository
{
    Task<List<Transport>> GetTransports();

    Task<Transport?> GetTransport(Guid id);


    Task<TransportCategory?> GetCategoryByTransport(Guid transportId);

    Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);

    Task DeleteTransport(Guid id);

    Task UpdateTransport(Guid categoryId, TransportForUpdateDto transport);

    Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport);
}