using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class LogTransactionRepository : BaseRepository<LogTransaction>, ILogTransactionRepository
{
    private readonly RentDbContext _context;

    public LogTransactionRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task CreateLogTransaction(LogTransaction transaction)
    {
        await Create(transaction);
    }

    public void DeleteLogTransaction(Guid logTransactionId)
    {
        Delete(logTransactionId);
    }

    public async Task<LogTransaction> GetLogTransaction(Guid id)
    {
        return await GetByCondition(x => x.Id == id);
    }

    public async Task<IEnumerable<LogTransaction>> GetLogTransactions()
    {
        return await GetAll();
    }

    public async Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId)
    {
        return await _context.LogTransactions.Include(x => x.Transaction).Where(x => x.Id == logTransactionId).Select(x => x.Transaction).FirstOrDefaultAsync();
    }

    public async Task<bool> LogTransactionExists(Guid id)
    {
        return await Exists(id);
    }

    public void UpdateLogTransaction(LogTransaction transaction)
    {
        Update(transaction);
    }
}