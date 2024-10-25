using kitchenette_server.Models;

namespace kitchenette_server.Interfaces;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllRecipes();
}