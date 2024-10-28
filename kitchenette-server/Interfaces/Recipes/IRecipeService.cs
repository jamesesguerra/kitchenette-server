using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Recipes;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
}