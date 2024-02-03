using Entities.Models;

namespace Interfaces.IRepository;

public interface IShopRepository
{
    Task<IEnumerable<Shop>> GetShops();

    Task<Shop> GetShop(Guid id);

    Task<Shop> GetShop(string adressShop);
    Task<bool> ShopExists(Guid id);

    Task<bool> ShopExists(string adressName);

    Task CreateShop(Shop shop);

    void DeleteShop(Guid id);

    void UpdateShop(Shop shop);
}