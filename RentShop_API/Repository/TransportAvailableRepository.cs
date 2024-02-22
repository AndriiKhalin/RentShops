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
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId)
    {
        return await GetByCondition(x => x.Id == transportAvailableId).Include(x => x.Transport).Select(x => x.Transport)
            .FirstOrDefaultAsync();
    }

    public async Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId)
    {
        return await GetByCondition(x => x.Id == transportAvailableId).Include(x => x.Shop).Select(x => x.Shop)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TransportAvailableExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task CreateTransportAvailable(Guid transportId, Guid shopId, TransportAvailable transportAvailable)
    {
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);
        var shopEntity = await _context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);

        transportAvailable.Transport = transportEntity;
        transportAvailable.Shop = shopEntity;

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