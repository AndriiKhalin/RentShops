namespace RentShop_API.Interfaces;

public interface ITransportAvailableRepository
{
    Task<List<TransportAvailable>> GetTransportAvailables();

    Task<TransportAvailable> GetTransportAvailable(Guid id);

    Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId);
    Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId);

    Task<bool> TransportAvailableExists(Guid id);
}