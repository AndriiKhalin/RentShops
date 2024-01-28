
namespace RentShop_API.Repository;

public class TransportAvailableRepository : ITransportAvailableRepository
{
    private readonly RentDbContext _context;

    public TransportAvailableRepository(RentDbContext context)
    {
        _context = context;
    }
    public async Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId)
    {
        return await _context.TransportAvailables.Include(x => x.Shop).Where(x => x.Id == transportAvailableId).Select(x => x.Shop).FirstOrDefaultAsync();
    }

    public async Task<TransportAvailable> GetTransportAvailable(Guid id)
    {
        return await _context.TransportAvailables.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TransportAvailable>> GetTransportAvailables()
    {
        return await _context.TransportAvailables.ToListAsync();
    }

    public async Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId)
    {
        return await _context.TransportAvailables.Include(x => x.Transport).Where(x => x.Id == transportAvailableId).Select(x => x.Transport).FirstOrDefaultAsync();
    }

    public async Task<bool> TransportAvailableExists(Guid id)
    {
        return await _context.TransportAvailables.AnyAsync(x => x.Id == id);
    }
}