
namespace RentShop_API.Repository;

public class RatingRepository : IRatingRepository
{
    private readonly RentDbContext _context;

    public RatingRepository(RentDbContext context)
    {
        _context = context;
    }
    public async Task<Rating> GetRating(Guid id)
    {
        return await _context.Ratings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Rating>> GetRatings()
    {
        return await _context.Ratings.ToListAsync();
    }

    public async Task<List<Rating>> GetRatingsByUser(Guid userId)
    {
        return await _context.Users.Include(x => x.Ratings).Where(x => x.Id == userId).SelectMany(x => x.Ratings).ToListAsync();
    }

    public async Task<User> GetUserByRating(Guid ratingId)
    {
        return await _context.Ratings.Include(x => x.User).Where(x => x.Id == ratingId).Select(x => x.User).FirstOrDefaultAsync();
    }

    public async Task<bool> RatingExists(Guid id)
    {
        return await _context.Ratings.AnyAsync(x => x.Id == id);
    }
}