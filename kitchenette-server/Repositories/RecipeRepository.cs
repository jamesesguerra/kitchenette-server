using Dapper;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Models;

namespace kitchenette_server.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly IDbContext _context;

    public RecipeRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Recipe>> GetRecipesByUserId(string userId)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @" SELECT R.Id,
                            R.CollectionId,
                            R.Name,
                            R.Description,
                            R.CoverPicture,
                            R.PrepTime,
                            R.CookTime,
                            R.Ingredients,
                            R.Instructions,
                            R.Servings,
                            R.Calories,
                            R.Protein,
                            R.Fat,
                            R.Carbohydrates,
                            R.CreatedAt 
                    FROM Recipe AS R 
                    INNER JOIN Collection AS C ON R.CollectionId = C.Id 
                    WHERE C.UserId = @userId ";
        
        var recipes = await connection.QueryAsync<Recipe>(sql, new { userId });
        return recipes;
    }
    
    public async Task<Recipe> GetRecipeById(int id)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @" SELECT Id,
                            CollectionId,
                            Name,
                            Description,
                            CoverPicture,
                            PrepTime,
                            CookTime,
                            Ingredients,
                            Instructions,
                            Servings,
                            Calories,
                            Protein,
                            Fat,
                            Fiber,
                            Carbohydrates,
                            CreatedAt 
                    FROM Recipe
                    WHERE Id = @id ";
        
        var recipe = await connection.QuerySingleAsync<Recipe>(sql, new { id });
        return recipe;
    }
    
    public async Task<Recipe> AddRecipe(Recipe recipe)
    {
        using var connection = _context.CreateConnection();

        var sql = @" INSERT INTO Recipe (
                         CollectionId,
                         Name,
                         Description,
                         CoverPicture,
                         PrepTime,
                         CookTime,
                         Ingredients,
                         Instructions,
                         Servings,
                         Calories,
                         Protein,
                         Fat,
                         Fiber,
                         Carbohydrates,
                         CreatedAt
                     ) VALUES (
                         @CollectionId,
                         @Name,
                         @Description,
                         @CoverPicture,
                         @PrepTime,
                         @CookTime,
                         @Ingredients,
                         @Instructions,
                         @Servings,
                         @Calories,
                         @Protein,
                         @Fat,
                         @Fiber,
                         @Carbohydrates,
                         @CreatedAt ) RETURNING Id ";
        
        var now = DateTime.UtcNow;
        var id = await connection.ExecuteScalarAsync<int>(sql, new
        {
            recipe.CollectionId,
            recipe.Name,
            recipe.Description,
            recipe.CoverPicture,
            recipe.PrepTime,
            recipe.CookTime,
            recipe.Ingredients,
            recipe.Instructions,
            recipe.Servings,
            recipe.Calories,
            recipe.Protein,
            recipe.Fat,
            recipe.Fiber,
            recipe.Carbohydrates,
            CreatedAt = now
        });
        
        recipe.Id = id;
        recipe.CreatedAt = now;

        return recipe;
    }
    
    
}