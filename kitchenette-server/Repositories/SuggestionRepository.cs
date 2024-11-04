using Dapper;
using kitchenette_server.Dtos;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Suggestions;
using kitchenette_server.Models;

namespace kitchenette_server.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly IDbContext _context;
    
    public SuggestionRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<SuggestionDto> GetSuggestionByIdWithChanges(int id)
    {
        using var connection = _context.CreateConnection();

        var suggestionSql = @"
            SELECT
                S.Id,
                S.RecipeId,
                S.Title,
                S.Description,
                S.Status,
                U.Nickname AS CreatedBy,
                S.CreatedAt
            FROM Suggestion S
            INNER JOIN Users U ON S.CreatedBy = U.Id
            WHERE S.Id = @id ";

        var changesSql = @"
            SELECT
                Id,
                SuggestionId,
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
                Carbohydrates
            FROM RecipeChanges
            WHERE SuggestionId = @id ";
        
        var suggestion = await connection.QueryFirstOrDefaultAsync<SuggestionDto>(suggestionSql, new { id });
        if (suggestion is null) throw new KeyNotFoundException($"Suggestion with ID {id} not found.");

        var changes = await connection.QuerySingleAsync<RecipeChanges>(changesSql, new { id });
        suggestion.RecipeChanges = changes;
        return suggestion;
    }

    public async Task<IEnumerable<SuggestionDto>> GetSuggestionsByRecipeId(int id)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT S.Id, S.Title, S.Status, S.CreatedAt, U.Nickname AS CreatedBy
                     FROM Suggestion S
                     INNER JOIN Users U ON S.CreatedBy = U.Id
                     WHERE S.RecipeId = @id ";
        
        var suggestions = await connection.QueryAsync<SuggestionDto>(sql, new { id });
        return suggestions;
    }
    
    public async Task<SuggestionDto> AddSuggestion(SuggestionDto suggestion)
    {
        using var connection = _context.CreateConnection();

        var suggestionSql = @" INSERT INTO Suggestion (RecipeId, Title, Description, CreatedBy)
                     VALUES (@RecipeId, @Title, @Description, @CreatedBy) RETURNING Id ";

        var id = await connection.ExecuteScalarAsync<int>(suggestionSql, new
        {
            suggestion.RecipeId,
            suggestion.Title,
            suggestion.Description,
            suggestion.CreatedBy
        });

        var changesSql = @" INSERT INTO RecipeChanges (
                                SuggestionId,
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
                                Carbohydrates)
                           VALUES (
                                @SuggestionId, @Name, @Description, @CoverPicture,
                                @PrepTime, @CookTime, @Ingredients, @Instructions,
                                @Servings, @Calories, @Protein, @Fat, @Fiber, @Carbohydrates)";
        
        suggestion.Id = id;
        var recipeChanges = suggestion.RecipeChanges;

        await connection.ExecuteAsync(changesSql, new
        {
            SuggestionId = suggestion.Id,
            recipeChanges.Name,
            recipeChanges.Description,
            recipeChanges.CoverPicture,
            recipeChanges.PrepTime,
            recipeChanges.CookTime,
            recipeChanges.Ingredients,
            recipeChanges.Instructions,
            recipeChanges.Servings,
            recipeChanges.Calories,
            recipeChanges.Protein,
            recipeChanges.Fat,
            recipeChanges.Fiber,
            recipeChanges.Carbohydrates
        });

        return suggestion;
    }

    public async Task PatchSuggestion(int id, Suggestion suggestion)
    {
        using var connection = _context.CreateConnection();
        
        var sql = " UPDATE Suggestion SET Status = @Status WHERE Id = @id ";

        await connection.ExecuteAsync(sql, new { id, suggestion.Status });
    }

    public async Task<int> DeleteSuggestion(int id)
    {
        using var connection = _context.CreateConnection();

        var sql = " DELETE FROM Suggestion WHERE Id = @id ";
        
        var affectedRows = await connection.ExecuteAsync(sql, new { id });

        return affectedRows;
    }
}