using Entities.Models;

namespace Interfaces.IRepository;

public interface ILogTransactionRepository
{
    Task<IEnumerable<LogTransaction>> GetLogTransactions();

    Task<LogTransaction> GetLogTransaction(Guid id);

    Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId);

    Task<bool> LogTransactionExists(Guid id);

    Task CreateLogTransaction(LogTransaction logTransaction);

    void DeleteLogTransaction(Guid logTransactionId);

    void UpdateLogTransaction(LogTransaction logTransaction);

}