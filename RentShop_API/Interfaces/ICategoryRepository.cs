namespace RentShop_API.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategories();

    Task<Category> GetCategory(Guid id);

    Task<Category> GetCategoryByTransport(Guid transportId);

    Task<List<Transport>> GetTransportsByCategory(Guid categoryId);

    Task<bool> CategoryExists(Guid id);
}