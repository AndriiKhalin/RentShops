using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RatingRepository : BaseRepository<Rating>, IRatingRepository
{
    private readonly RentDbContext _context;

    public RatingRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rating>> GetRatings()
    {
        return await GetAll();
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

    public async Task CreateRating(Guid userId, Guid transportId, Rating rating)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);

        rating.Transport = transportEntity;
        rating.User = userEntity;

        await Create(rating);
    }

    public void DeleteRating(Guid id)
    {
        Delete(id);
    }

    public void UpdateRating(Rating rating)
    {
        Update(rating);
    }
}