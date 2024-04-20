namespace Interfaces.IRepository;

public interface IUnitOfWorkRepository
{
    IUserRepository User { get; }

    ICategoryRepository Category { get; }
    ILogTransactionRepository LogTransaction { get; }
    IOrderRepository Order { get; }
    IRatingRepository Rating { get; }
    IShopRepository Shop { get; }
    ITransactionRepository Transaction { get; }
    ITransportAvailableRepository TransportAvailable { get; }
    ITransportRepository Transport { get; }
    Task Save();

}