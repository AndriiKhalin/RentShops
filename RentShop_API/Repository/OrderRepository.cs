using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly RentDbContext _context;

    public OrderRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task CreateOrder(Guid userId, Guid shopId, Guid transportId, Order order)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var shopEntity = await _context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);

        order.Transport = transportEntity;
        order.Shop = shopEntity;
        order.User = userEntity;

        await Create(order);
    }

    public void DeleteOrder(Guid id)
    {
        Delete(id);
    }

    public async Task<Order> GetOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await GetAll();
    }

    public async Task<User> GetUserByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.User).Select(x => x.User)
            .FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.Transport).Select(x => x.Transport)
            .FirstOrDefaultAsync();
    }

    public async Task<Transaction> GetTransactionByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.Transaction).Select(x => x.Transaction)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> OrderExists(Guid id)
    {
        return await Exists(id);
    }

    public void UpdateOrder(Order order)
    {
        Update(order);
    }


}