
namespace RentShop_API.Repository;

public class TransportRepository : ITransportRepository
{
    private readonly RentDbContext _context;

    public TransportRepository(RentDbContext context)
    {
        _context = context;
    }
    public async Task<List<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await _context.Transports.Include(x => x.Orders).Where(x => x.Id == transportId).SelectMany(x => x.Orders).ToListAsync();
    }

    public async Task<Transport> GetTransport(Guid id)
    {
        return await _context.Transports.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Transport> GetTransportByOrder(Guid orderId)
    {
        return await _context.Orders.Include(x => x.Transport).Where(x => x.Id == orderId).Select(x => x.Transport).FirstOrDefaultAsync();
    }

    public async Task<List<Transport>> GetTransports()
    {
        return await _context.Transports.ToListAsync();
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await _context.Transports.AnyAsync(x => x.Id == id);
    }
}