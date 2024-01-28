namespace RentShop_API.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactions();

    Task<Transaction> GetTransaction(Guid id);

    Task<Transaction> GetTransactionByOrder(Guid orderId);

    Task<Order> GetOrderByTransaction(Guid transactionId);

    Task<bool> TransactionExists(Guid id);
}