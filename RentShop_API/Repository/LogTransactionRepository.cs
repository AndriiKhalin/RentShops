
namespace RentShop_API.Repository;

public class LogTransactionRepository : ILogTransactionRepository
{
    private readonly RentDbContext _context;

    public LogTransactionRepository(RentDbContext context)
    {
        _context = context;
    }

    public async Task<LogTransaction> GetLogTransaction(Guid id)
    {
        return await _context.LogTransactions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LogTransaction>> GetLogTransactions()
    {
        return await _context.LogTransactions.ToListAsync();
    }

    public async Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId)
    {
        return await _context.LogTransactions.Include(x => x.Transaction).Where(x => x.Id == logTransactionId).Select(x => x.Transaction).FirstOrDefaultAsync();
    }

    public async Task<bool> LogTransactionExists(Guid id)
    {
        return await _context.LogTransactions.AnyAsync(x => x.Id == id);
    }
}