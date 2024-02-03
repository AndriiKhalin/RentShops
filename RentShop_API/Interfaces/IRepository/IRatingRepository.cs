using Entities.Models;

namespace Interfaces.IRepository;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetRatings();

    Task<Rating> GetRating(Guid id);

    Task<User> GetUserByRating(Guid ratingId);

    Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId);

    Task<bool> RatingExists(Guid id);

    Task CreateRating(Rating rating);
    void DeleteRating(Guid id);

    void UpdateRating(Rating rating);
}