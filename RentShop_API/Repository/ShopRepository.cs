using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ShopRepository : BaseRepository<Shop>, IShopRepository
{
    private readonly RentDbContext _context;

    public ShopRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shop>> GetShops()
    {
        return await GetAll();
    }

    public async Task<Shop> GetShop(Guid id)
    {
        return await GetByCondition(x => x.Id == id);
    }

    public async Task<Shop> GetShop(string adressShop)
    {
        return await GetByCondition(x => x.Address.Contains(adressShop));
    }

    public async Task<bool> ShopExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task CreateShop(Shop shop)
    {
        await Create(shop);
    }

    public void DeleteShop(Guid id)
    {
        Delete(id);
    }

    public void UpdateShop(Shop shop)
    {
        Update(shop);
    }

    public async Task<bool> ShopExists(string adressName)
    {
        return await _context.Shops.AnyAsync(x => x.Address.Contains(adressName));
    }
}