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

    public async Task<IEnumerable<Recipe>> GetAllRecipes()
    {
        return await _recipeRepository.GetAllRecipes();
    }
}