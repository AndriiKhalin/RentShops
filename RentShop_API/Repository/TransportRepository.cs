using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TransportRepository : BaseRepository<Transport>, ITransportRepository
{
    private readonly RentDbContext _context;

    public TransportRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transport>> GetTransports()
    {
        return await GetAll();
    }

    public async Task<Transport> GetTransport(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await _context.Transports.Include(x => x.Orders).Where(x => x.Id == transportId).SelectMany(x => x.Orders).ToListAsync();
    }

    public async Task<Category> GetCategoryByTransport(Guid transportId)
    {
        return await _context.Transports.Include(x => x.Category).Where(x => x.Id == transportId).Select(x => x.Category).FirstOrDefaultAsync();
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await Exists(id);
    }

    public void DeleteTransport(Guid id)
    {
        Delete(id);
    }

    public void UpdateTransport(Transport transport)
    {
        Update(transport);
    }

    public async Task CreateTransport(Transport transport)
    {
        await Create(transport);
    }
}