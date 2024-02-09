using Entities.Models;

namespace Interfaces.IRepository;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetRatings();

    Task<Rating> GetRating(Guid id);

    Task<User> GetUserByRating(Guid ratingId);


    Task<bool> RatingExists(Guid id);

    Task CreateRating(Rating rating);
    void DeleteRating(Guid id);

    void UpdateRating(Rating rating);
}