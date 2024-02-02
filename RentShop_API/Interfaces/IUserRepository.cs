using System.Runtime.InteropServices.JavaScript;

namespace RentShop_API.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task<User> GetUser(Guid id);

    Task<User> GetUser(string username);

    Task<DateTime?> GetLastUserOrder(Guid id);

    Task<User> CreateUser(User user);

    Task<bool> UserExists(Guid id);

    //Task<bool> CreateUser(User user);

    //Task<bool> Save();
}