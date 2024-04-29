using Models.DTO.UserDTO;
using Models.Entities;

namespace Interfaces.IEntityService;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();

    Task<User> GetUser(Guid id);

    Task<User> GetUser(string username);

    Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId);

    Task<DateTime?> GetLastUserOrder(Guid id);

    Task<User> CreateUser(UserForCreateDto user);

    Task UpdateUser(Guid userId, UserForUpdateDto user);

    Task DeleteUser(Guid id);

    Task<bool> UserExists(Guid id);

    Task<bool> UserExists(string userName);
}