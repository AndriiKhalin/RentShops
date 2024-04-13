
using Models.DTO.RatingDTO;
using Models.Entities;

namespace Interfaces.IRepository;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetRatings();

    Task<Rating> GetRating(Guid id);

    Task<User> GetUserByRating(Guid ratingId);

    Task<Transport> GetTransportByRating(Guid ratingId);
    Task<bool> RatingExists(Guid id);

    Task<Rating> CreateRating(Guid userId, Guid transportId, RatingForCreateDto rating);
    void DeleteRating(Guid id);

    Task UpdateRating(Guid ratingId, RatingForUpdateDto rating);
}