using kitchenette_server.Interfaces.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipes = await _recipeService.GetAllRecipes();
        return Ok(recipes);
    }
}
