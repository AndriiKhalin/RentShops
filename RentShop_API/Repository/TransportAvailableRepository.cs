using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TransportAvailableRepository : BaseRepository<TransportAvailable>, ITransportAvailableRepository
{
    private readonly RentDbContext _context;

    public TransportAvailableRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransportAvailable>> GetTransportAvailables()
    {
        return await GetAll();
    }

    public async Task<TransportAvailable> GetTransportAvailable(Guid id)
    {
        return await GetByCondition(x => x.Id == id);
    }

    public async Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId)
    {
        return await _context.TransportAvailables.Include(x => x.Transport).Where(x => x.Id == transportAvailableId).Select(x => x.Transport).FirstOrDefaultAsync();
    }

    public async Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId)
    {
        return await _context.TransportAvailables.Include(x => x.Shop).Where(x => x.Id == transportAvailableId).Select(x => x.Shop).FirstOrDefaultAsync();
    }

    public async Task<bool> TransportAvailableExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task CreateTransportAvailable(TransportAvailable transportAvailable)
    {
        await Create(transportAvailable);
    }

    public void DeleteTransportAvailable(Guid id)
    {
        Delete(id);
    }

    public void UpdateTransportAvailable(TransportAvailable transportAvailable)
    {
        Update(transportAvailable);
    }
}