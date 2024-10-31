using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetRecipesByUserId([FromQuery] string userId)
    {
        var recipes = await _recipeService.GetRecipesByUserId(userId);
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById([FromRoute] int id)
    {
        var recipe = await _recipeService.GetRecipeById(id);
        return Ok(recipe);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddRecipe(Recipe newRecipe)
    {
        var recipe = await _recipeService.AddRecipe(newRecipe);
        return Ok(recipe);
    }
}
