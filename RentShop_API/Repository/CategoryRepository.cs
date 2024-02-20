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
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Transport>> GetTransportsByCategory(Guid categoryId)
    {
        return await GetByCondition(x => x.Id == categoryId).Include(x => x.Transports).SelectMany(x => x.Transports)
            .ToListAsync();
    }

    public void UpdateCategory(Category category)
    {
        Update(category);
    }
}