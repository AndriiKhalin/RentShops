
namespace RentShop_API.Repository;

public class ShopRepository : IShopRepository
{
    private readonly RentDbContext _context;

    public ShopRepository(RentDbContext context)
    {
        _context = context;
    }

    public async Task<Shop> GetShop(Guid id)
    {
        return await _context.Shops.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Shop> GetShop(string adressShop)
    {
        return await _context.Shops.FirstOrDefaultAsync(x => x.Address.Contains(adressShop));
    }

    public async Task<List<Shop>> GetShops()
    {
        return await _context.Shops.ToListAsync();
    }

    public async Task<bool> ShopExists(Guid id)
    {
        return await _context.Shops.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ShopExists(string adressShop)
    {
        return await _context.Shops.AnyAsync(x => x.Address.Contains(adressShop));
    }
}