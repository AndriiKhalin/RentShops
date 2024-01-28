namespace RentShop_API.Interfaces;

public interface IShopRepository
{
    Task<List<Shop>> GetShops();

    Task<Shop> GetShop(Guid id);

    Task<Shop> GetShop(string adressShop);
    Task<bool> ShopExists(Guid id);
    Task<bool> ShopExists(string adressShop);
}