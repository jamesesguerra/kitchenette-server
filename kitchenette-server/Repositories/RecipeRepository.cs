using Dapper;
using kitchenette_server.Interfaces;
using kitchenette_server.Models;

namespace kitchenette_server.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly IDbContext _context;

    public RecipeRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        using var connection = _context.CreateConnection();

        var sql = " SELECT * FROM Recipe ";
        var recipes = await connection.QueryAsync<Recipe>(sql);
        return recipes;
    }
}