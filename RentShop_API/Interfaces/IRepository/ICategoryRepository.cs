using Entities.Models;

namespace Interfaces.IRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();

    Task<Category> GetCategory(Guid id);

    Task<IEnumerable<Transport>> GetTransportsByCategory(Guid categoryId);

    Task<bool> CategoryExists(Guid id);

    Task CreateCategory(Category category);

    void DeleteCategory(Guid id);

    void UpdateCategory(Category category);
}
