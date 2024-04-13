
using Models.DTO.LogTransactionDTO;
using Models.Entities;

namespace Interfaces.IRepository;

public interface ILogTransactionRepository
{
    Task<IEnumerable<LogTransaction>> GetLogTransactions();

    Task<LogTransaction> GetLogTransaction(Guid id);

    Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId);

    Task<bool> LogTransactionExists(Guid id);

    Task<LogTransaction> CreateLogTransaction(Guid transactionId, LogTransactionForCreateDto logTransaction);

    void DeleteLogTransaction(Guid logTransactionId);

    Task UpdateLogTransaction(Guid logTransactionId, LogTransactionForUpdateDto logTransaction);

}