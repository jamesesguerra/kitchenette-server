using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Models;
using Dapper;
using kitchenette_server.Dtos;
using kitchenette_server.Interfaces.Collections;

namespace kitchenette_server.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly IDbContext _context;

    public CollectionRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT Id,
                            UserId,
                            Name,
                            Description,
                            IsVisible,
                            CreatedAt
                     FROM Collection
                     WHERE UserId = @userId ";
        var collections = await connection.QueryAsync<Collection>(sql, new { userId });
        return collections;
    }
    
    public async Task<Collection?> GetCollectionById(int id)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT Id,
                            UserId,
                            Name,
                            Description,
                            IsVisible,
                            CreatedAt
                     FROM Collection
                     WHERE Id = @id ";
        var collection = await connection.QuerySingleOrDefaultAsync<Collection>(sql, new { id });
        return collection;
    }

    public async Task<Collection> AddCollection(Collection newCollection)
    {
        using var connection = _context.CreateConnection();

        var sql = @" INSERT INTO Collection (
                         UserId,
                         Name,
                         Description,
                         IsVisible,
                         CreatedAt
                     ) VALUES (
                         @UserId,
                         @Name,
                         @Description,
                         @IsVisible,
                         @CreatedAt ) RETURNING Id ";
        
        var now = DateTime.UtcNow;
        var id = await connection.ExecuteScalarAsync<int>(sql, new
        {
            newCollection.UserId,
            newCollection.Name,
            newCollection.Description,
            newCollection.IsVisible,
            CreatedAt = now
        });
        
        newCollection.Id = id;
        newCollection.CreatedAt = now;

        return newCollection;
    }

    public async Task<int> DeleteCollection(int collectionId)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @" DELETE FROM Collection WHERE Id = @collectionId ";
        var affectedRows = await connection.ExecuteAsync(sql, new { collectionId });

        return affectedRows;
    }

    public async Task<CollectionDto> GetCollectionByIdWithRecipes(int id)
    {
        using var connection = _context.CreateConnection();

        var collectionSql = @"
            SELECT
                Id,
                Name,
                Description,
                IsVisible,
                CreatedAt
            FROM Collection
            WHERE Id = @id ";

        var recipesSql = @"
            SELECT
                Id,
                Name,
                Description,
                CoverPicture
            FROM Recipe
            WHERE CollectionId = @id ";
        
        var collection = await connection.QueryFirstOrDefaultAsync<CollectionDto>(collectionSql, new { id });
        if (collection is null) throw new KeyNotFoundException($"Collection with ID {id} not found.");

        var recipes = await connection.QueryAsync<Recipe>(recipesSql, new { id });
        collection.Recipes = recipes;
        return collection;
    }

    public async Task UpdateCollection(int id, Collection updatedCollection)
    {
        using var connection = _context.CreateConnection();

        var sql = @" UPDATE Collection
                     SET Name = @Name, 
                         Description = @Description
                     WHERE Id = @id ";

        await connection.ExecuteAsync(sql, new
        {
            updatedCollection.Name,
            updatedCollection.Description,
            id
        });
    }
}