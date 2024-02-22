using Entities.Models;

namespace Interfaces.IRepository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrders();

    Task<Order> GetOrder(Guid orderId);

    Task<User> GetUserByOrder(Guid orderId);

    Task<Transport> GetTransportByOrder(Guid orderId);

    Task<Transaction> GetTransactionByOrder(Guid orderId);

    Task<bool> OrderExists(Guid id);

    Task CreateOrder(Guid userId, Guid shopId, Guid transportId, Order order);

    void DeleteOrder(Guid id);

    void UpdateOrder(Order order);
}