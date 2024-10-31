using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService (IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<IEnumerable<Recipe>> GetRecipesByUserId(string userId)
    {
        return await _recipeRepository.GetRecipesByUserId(userId);
    }

    public async Task<Recipe> GetRecipeById(int id)
    {
        return await _recipeRepository.GetRecipeById(id);
    }

    public async Task<Recipe> AddRecipe(Recipe newRecipe)
    {
        return await _recipeRepository.AddRecipe(newRecipe);
    }
}