using kitchenette_server.Interfaces.Users;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task PatchUser(User user)
    {
        await _userRepository.PatchUser(user);
    }
    
    public async Task<User> GetUsersAsync(string id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task AddUser(User user)
    {
        await _userRepository.AddUser(user);
    }
}