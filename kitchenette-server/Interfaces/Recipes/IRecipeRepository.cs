using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Recipes;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
}