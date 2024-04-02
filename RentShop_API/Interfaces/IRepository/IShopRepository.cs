using Entities.DTO.ShopDTO;
using Entities.Models;

namespace Interfaces.IRepository;

public interface IShopRepository
{
    Task<IEnumerable<Shop>> GetShops();

    Task<Shop> GetShop(Guid id);

    Task<Shop> GetShop(string adressShop);
    Task<bool> ShopExists(Guid id);

    Task<bool> ShopExists(string adressName);

    Task<Shop> CreateShop(ShopForCreateDto shop);

    void DeleteShop(Guid id);

    Task UpdateShop(Guid shopId, ShopForUpdateDto shop);
}