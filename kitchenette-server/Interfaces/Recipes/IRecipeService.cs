using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Recipes;

public interface IRecipeService
{
    Task<IEnumerable<RecipeSummaryDto>> GetRecipeSummariesByUserId(string userId);
    Task<Recipe> GetRecipeById(int id);
    Task<Recipe> AddRecipe(Recipe newRecipe);
    Task UpdateRecipe(Recipe recipe);
    Task<int> DeleteRecipesByIds(string ids);
}