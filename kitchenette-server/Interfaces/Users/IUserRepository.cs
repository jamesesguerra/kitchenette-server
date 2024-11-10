using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Users;

public interface IUserRepository
{
    Task PatchUser(User user);
    Task<User> GetUserById(string id);
    Task AddUser(User user);
}