using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Recipes;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetRecipesByUserId(string userId);
    Task<Recipe> GetRecipeById(int id);
    Task<Recipe> AddRecipe(Recipe newRecipe);
}