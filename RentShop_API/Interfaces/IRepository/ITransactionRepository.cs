using Entities.Models;

namespace Interfaces.IRepository;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactions();

    Task<Transaction> GetTransaction(Guid id);


    Task<Order> GetOrderByTransaction(Guid transactionId);

    Task<bool> TransactionExists(Guid id);

    Task CreateTransaction(Guid orderId, Transaction transaction);

    void DeleteTransaction(Guid transactionId);

    void UpdateTransaction(Transaction transaction);
}