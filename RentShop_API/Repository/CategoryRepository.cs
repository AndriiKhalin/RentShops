using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Entities;
using Entities.DTO.CategoryDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class CategoryRepository : BaseRepository<TransportCategory>, ICategoryRepository
{
    private RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public CategoryRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }
    public async Task<bool> CategoryExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task<TransportCategory> CreateCategory(TransportCategoryForCreateDto category)
    {
        var categoryMap = _mapper.Map<TransportCategory>(category);
        categoryMap.CreatedUpdatedAt = DateTime.Now;

        await Create(categoryMap);
        return categoryMap;
    }

    public void DeleteCategory(Guid id)
    {
        Delete(id);
    }

    public async Task<IEnumerable<TransportCategory>> GetCategories()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<TransportCategory> GetCategory(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Transport>> GetTransportsByCategory(Guid categoryId)
    {
        return await GetByCondition(x => x.Id == categoryId).Include(x => x.Transports).SelectMany(x => x.Transports)
            .ToListAsync();
    }

    public async Task UpdateCategory(Guid categoryId, TransportCategoryForUpdateDto category)
    {
        var categoryEntity = await GetByCondition(x => x.Id == categoryId).FirstOrDefaultAsync();

        _mapper.Map(category, categoryEntity);

        categoryEntity.CreatedUpdatedAt = DateTime.Now;

        Update(categoryEntity);
    }
}