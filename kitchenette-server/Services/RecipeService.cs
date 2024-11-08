using kitchenette_server.Dtos;
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

    public async Task<IEnumerable<RecipeSummaryDto>> GetRecipeSummariesByUserId(string userId)
    {
        return await _recipeRepository.GetRecipeSummariesByUserId(userId);
    }

    public async Task<RecipeDto> GetRecipeById(int id)
    {
        return await _recipeRepository.GetRecipeById(id);
    }

    public async Task<Recipe> AddRecipe(Recipe newRecipe)
    {
        return await _recipeRepository.AddRecipe(newRecipe);
    }
    
    public async Task UpdateRecipe(Recipe recipe)
    {
        await _recipeRepository.UpdateRecipe(recipe);
    }

    public async Task PatchRecipe(int id, RecipeChanges recipe)
    {
        await _recipeRepository.PatchRecipe(id, recipe);
    }

    public async Task<int> DeleteRecipesByIds(string ids)
    {
        return await _recipeRepository.DeleteRecipesByIds(ids);
    }

    public Task<IEnumerable<RecipeDto>> GetRandomRecipes()
    {
        return _recipeRepository.GetRandomRecipes();
    }
}