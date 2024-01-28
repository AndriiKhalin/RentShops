

namespace RentShop_API.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly RentDbContext _context;

    public TransactionRepository(RentDbContext context)
    {
        _context = context;
    }
    public async Task<Order> GetOrderByTransaction(Guid transactionId)
    {
        return await _context.Transactions.Include(x => x.Order).Where(x => x.Id == transactionId).Select(x => x.Order).FirstOrDefaultAsync();
    }

    public async Task<Transaction> GetTransaction(Guid id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Transaction> GetTransactionByOrder(Guid orderId)
    {
        return await _context.Orders.Include(x => x.Transaction).Where(x => x.Id == orderId).Select(x => x.Transaction).FirstOrDefaultAsync();
    }

    public async Task<List<Transaction>> GetTransactions()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<bool> TransactionExists(Guid id)
    {
        return await _context.Transactions.AnyAsync(x => x.Id == id);
    }
}