using System.Text;
using Dapper;
using kitchenette_server.Dtos;
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
    
    public async Task<IEnumerable<RecipeSummaryDto>> GetRecipeSummariesByUserId(string userId)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @" SELECT R.Id,
                            C.Name AS Collection,
                            R.Name,
                            R.Description,
                            R.CoverPicture,
                            R.CreatedAt,
                            ROUND(AVG(RR.Rating), 0) AS AverageRating
                    FROM Recipe AS R 
                    INNER JOIN Collection AS C ON R.CollectionId = C.Id 
                    LEFT JOIN RecipeReview AS RR ON R.Id = RR.RecipeId
                    WHERE C.UserId = @userId
                    GROUP BY R.Id, C.Name, R.Name, R.Description, R.CoverPicture, R.CreatedAt; ";
        
        var recipes = await connection.QueryAsync<RecipeSummaryDto>(sql, new { userId });
        return recipes;
    }
    
    public async Task<RecipeDto> GetRecipeById(int id)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @" SELECT R.Id,
                            R.CollectionId,
                            C.Name AS CollectionName,
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
                            R.Fiber,
                            R.Carbohydrates,
                            U.Nickname AS CreatedBy,
                            R.CreatedAt 
                    FROM Recipe R
                    INNER JOIN Collection C ON R.CollectionId = C.Id
                    INNER JOIN Users U ON C.UserId = U.Id
                    WHERE R.Id = @id ";
        
        var recipe = await connection.QuerySingleAsync<RecipeDto>(sql, new { id });
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
                     ) OUTPUT INSERTED.Id 
                         VALUES (
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
                         @CreatedAt ) ";
        
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

    public async Task UpdateRecipe(Recipe recipe)
    {
        using var connection = _context.CreateConnection();

        var sql = @" UPDATE Recipe
                     SET
                        CollectionId = @CollectionId,
                        Name = @Name,
                        Description = @Description,
                        CoverPicture = @CoverPicture,
                        PrepTime = @PrepTime,
                        CookTime = @CookTime,
                        Ingredients = @Ingredients,
                        Instructions = @Instructions,
                        Servings = @Servings,
                        Calories = @Calories,
                        Protein = @Protein,
                        Fat = @Fat,
                        Fiber = @Fiber,
                        Carbohydrates = @Carbohydrates
                    WHERE
                        Id = @Id; ";

        await connection.ExecuteAsync(sql, recipe);
    }

    public async Task PatchRecipe(int id, RecipeChanges recipe)
    {
        using var connection = _context.CreateConnection();

        var queryBuilder = new StringBuilder(" UPDATE Recipe SET ");
        var dynamicParams = BuildDynamicParams(recipe, queryBuilder);
        queryBuilder.Append(" WHERE Id = @id ");
        dynamicParams.Add("@id", id);
        
        await connection.ExecuteAsync(queryBuilder.ToString(), dynamicParams);
    }

    public async Task<int> DeleteRecipesByIds(string ids)
    {
        using var connection = _context.CreateConnection();
        
        var sql = @$" DELETE FROM Recipe WHERE Id IN ({ids}) ";
        var affectedRows = await connection.ExecuteAsync(sql);

        return affectedRows;
    }
    
    #region private methods
    private DynamicParameters BuildDynamicParams(RecipeChanges recipe, StringBuilder queryBuilder)
    {
        var dynamicParams = new DynamicParameters();
        bool isFirstParam = true;

        var properties = typeof(RecipeChanges).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(recipe);
            if (value != null && property.Name != "Id" && property.Name != "SuggestionId")
            {
                var fieldName = property.Name;
                var paramName = $"@{fieldName}";

                queryBuilder.Append(isFirstParam ? $" {fieldName} = {paramName} " : $", {fieldName} = {paramName} ");
                dynamicParams.Add(paramName, value);
                isFirstParam = false;
            }
        }
        
        return dynamicParams;
    }
    #endregion
}
