using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private RentDbContext _context;
    public CategoryRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<bool> CategoryExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task CreateCategory(Category category)
    {
        await Create(category);
    }

    public void DeleteCategory(Guid id)
    {
        Delete(id);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await GetAll();
    }

    public async Task<Category> GetCategory(Guid id)
    {
        return await GetByCondition(x => x.Id == id);
    }

    public async Task<Category> GetCategoryByTransport(Guid transportId)
    {
        return await _context.Transports.Include(x => x.Category).Where(x => x.Id == transportId).Select(x => x.Category).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Transport>> GetTransportsByCategory(Guid categoryId)
    {
        return await _context.Categories.Include(x => x.Transports).Where(x => x.Id == categoryId).SelectMany(x => x.Transports).ToListAsync();
    }

    public void UpdateCategory(Category category)
    {
        Update(category);
    }
}