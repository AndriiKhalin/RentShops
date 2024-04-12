using Models.DTO.TransportCategoryDTO;
using Models.Entities;

namespace Services.Interfaces.IRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<TransportCategory>> GetCategories();

    Task<TransportCategory> GetCategory(Guid id);

    Task<IEnumerable<Transport>> GetTransportsByCategory(Guid categoryId);

    Task<bool> CategoryExists(Guid id);

    Task<TransportCategory> CreateCategory(TransportCategoryForCreateDto category);

    void DeleteCategory(Guid id);

    Task UpdateCategory(Guid categoryId, TransportCategoryForUpdateDto category);
}
