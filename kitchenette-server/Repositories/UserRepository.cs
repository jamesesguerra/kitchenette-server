using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Users;
using kitchenette_server.Models;
using Dapper;

namespace kitchenette_server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserById(string id)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT Id, 
                            Name,
                            Nickname,
                            Email,
                            Bio,
                            Picture,
                            CreatedAt
                     FROM Users
                     WHERE Id = @id ";
        var users = await connection.QuerySingleAsync<User>(sql, new { id });
        return users;
    }
}