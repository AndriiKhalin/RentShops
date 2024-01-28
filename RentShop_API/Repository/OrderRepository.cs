
namespace RentShop_API.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly RentDbContext _context;

    public OrderRepository(RentDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrder(Guid orderId)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<User> GetUserByOrder(Guid orderId)
    {
        return await _context.Orders.Include(o => o.User).Where(x => x.Id == orderId).Select(o => o.User)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> OrderExists(Guid id)
    {
        return await _context.Orders.AnyAsync(o => o.Id == id);
    }
}