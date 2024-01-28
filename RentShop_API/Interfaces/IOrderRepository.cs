namespace RentShop_API.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrders();

    Task<Order> GetOrder(Guid orderId);

    Task<User> GetUserByOrder(Guid orderId);

    Task<bool> OrderExists(Guid id);
}