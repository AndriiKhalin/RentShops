using Entities.Models;

namespace Interfaces.IRepository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();

    Task<User> GetUser(Guid id);

    Task<User> GetUser(string username);

    Task<DateTime?> GetLastUserOrder(Guid id);

    Task CreateUser(User user);

    void UpdateUser(User user);

    void DeleteUser(Guid id);
    Task<bool> UserExists(Guid id);
}