using kitchenette_server.Models;

namespace kitchenette_server.Interfaces;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
}