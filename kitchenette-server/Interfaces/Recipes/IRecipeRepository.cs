using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Recipes;

public interface IRecipeRepository
{
    Task<IEnumerable<RecipeSummaryDto>> GetRecipeSummariesByUserId(string userId);
    Task<RecipeDto> GetRecipeById(int id);
    Task<Recipe> AddRecipe(Recipe newRecipe);
    Task UpdateRecipe(Recipe recipe);
    Task PatchRecipe(int id, RecipeChanges recipe);
    Task<int> DeleteRecipesByIds(string ids);
}