using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Users;

public interface IUserService
{
    Task PatchUser(User user);
    Task<User> GetUsersAsync(string id);
    Task AddUser(User user);
}