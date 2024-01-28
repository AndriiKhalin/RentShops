
namespace RentShop_API.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly RentDbContext _context;

    public CategoryRepository(RentDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CategoryExists(Guid categoryId)
    {
        return await _context.Categories.AnyAsync(x => x.Id == categoryId);
    }

    public async Task<List<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategory(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category> GetCategoryByTransport(Guid transportId)
    {
        return await _context.Transports.Include(x => x.Category).Where(x => x.Id == transportId).Select(x => x.Category).FirstOrDefaultAsync();
    }

    public async Task<List<Transport>> GetTransportsByCategory(Guid categoryId)
    {
        return await _context.Categories.Include(x => x.Transports).Where(x => x.Id == categoryId).SelectMany(x => x.Transports).ToListAsync();
    }
}