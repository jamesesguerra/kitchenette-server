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

    public async Task PatchUser(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = @" UPDATE Users
                     SET Nickname = @nickname, Bio = @bio, Picture = @picture WHERE Id = @id ";
        await connection.ExecuteAsync(sql, new
        {
            user.Nickname, user.Bio, user.Id, user.Picture
        });
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

    public async Task AddUser(User user)
    {
        using var connection = _context.CreateConnection();

        var sql = @" INSERT INTO Users (Id, Name, Nickname, Email, Bio, Picture)
                     VALUES (@Id, @Name, @Nickname, @Email, @Bio, @Picture)";

        await connection.ExecuteAsync(sql, new
        {
            user.Id,
            user.Name,
            user.Nickname,
            user.Email,
            user.Bio,
            user.Picture
        });
    }
}