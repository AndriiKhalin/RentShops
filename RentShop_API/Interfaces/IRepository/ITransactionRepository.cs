using Models.DTO.TransactionDTO;
using Models.Entities;

namespace Interfaces.IRepository;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactions();

    Task<Transaction> GetTransaction(Guid id);


    Task<Order> GetOrderByTransaction(Guid transactionId);

    Task<bool> TransactionExists(Guid id);

    Task<Transaction> CreateTransaction(Guid orderId, TransactionForCreateDto transaction);

    void DeleteTransaction(Guid transactionId);

    Task UpdateTransaction(Guid transactionId, TransactionForUpdateDto transaction);
}