namespace RentShop_API.Interfaces;

public interface ITransportRepository
{
    Task<List<Transport>> GetTransports();

    Task<Transport> GetTransport(Guid id);

    Task<Transport> GetTransportByOrder(Guid orderId);

    Task<List<Order>> GetOrdersByTransport(Guid transportId);

    Task<bool> TransportExists(Guid id);


}