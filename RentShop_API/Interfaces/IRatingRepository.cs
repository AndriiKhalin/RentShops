namespace RentShop_API.Interfaces;

public interface IRatingRepository
{
    Task<List<Rating>> GetRatings();

    Task<Rating> GetRating(Guid id);

    Task<User> GetUserByRating(Guid ratingId);

    Task<List<Rating>> GetRatingsByUser(Guid userId);

    Task<bool> RatingExists(Guid id);
}