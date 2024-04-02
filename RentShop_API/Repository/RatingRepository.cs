using AutoMapper;
using Entities;
using Entities.DTO.RatingDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class RatingRepository : BaseRepository<Rating>, IRatingRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public RatingRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Rating>> GetRatings()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<Rating> GetRating(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByRating(Guid ratingId)
    {
        return await GetByCondition(x => x.Id == ratingId).Include(x => x.User).Select(x => x.User).FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByRating(Guid ratingId)
    {
        return await GetByCondition(x => x.Id == ratingId).Include(x => x.Transport).Select(x => x.Transport).FirstOrDefaultAsync();
    }

    public async Task<bool> RatingExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task<Rating> CreateRating(Guid userId, Guid transportId, RatingForCreateDto rating)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);

        var ratingMap = _mapper.Map<Rating>(rating);

        ratingMap.Transport = transportEntity;
        ratingMap.User = userEntity;
        ratingMap.CreatedUpdatedAt = DateTime.Now;

        await Create(ratingMap);
        return ratingMap;
    }

    public void DeleteRating(Guid id)
    {
        Delete(id);
    }

    public async Task UpdateRating(Guid ratingId, RatingForUpdateDto rating)
    {
        var ratingEntity = await GetByCondition(x => x.Id == ratingId).FirstOrDefaultAsync();

        _mapper.Map(rating, ratingEntity);

        ratingEntity.CreatedUpdatedAt = DateTime.Now;

        Update(ratingEntity);
    }
}