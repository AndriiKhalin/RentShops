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
    public async Task CreateOrder(Order order)
    {
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
        return await _context.Orders.Include(o => o.User).Where(x => x.Id == orderId).Select(o => o.User)
            .FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByOrder(Guid orderId)
    {
        return await _context.Orders.Include(x => x.Transport).Where(x => x.Id == orderId).Select(x => x.Transport).FirstOrDefaultAsync();
    }

    public async Task<Transaction> GetTransactionByOrder(Guid orderId)
    {
        return await _context.Orders.Include(x => x.Transaction).Where(x => x.Id == orderId).Select(x => x.Transaction).FirstOrDefaultAsync();
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