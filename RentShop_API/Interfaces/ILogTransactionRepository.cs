namespace RentShop_API.Interfaces;

public interface ILogTransactionRepository
{
    Task<List<LogTransaction>> GetLogTransactions();

    Task<LogTransaction> GetLogTransaction(Guid id);

    Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId);

    Task<bool> LogTransactionExists(Guid id);
}