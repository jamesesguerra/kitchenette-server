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
    public async Task<IActionResult> GetRecipeSummariesByUserId([FromQuery] string userId)
    {
        var recipes = await _recipeService.GetRecipeSummariesByUserId(userId);
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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
    {
        if (recipe is null) return BadRequest("Recipe data is required");
        if (recipe.Id != id) return BadRequest("Recipe id does not match");
        
        await _recipeService.UpdateRecipe(recipe);
        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchRecipe(int id, [FromBody] RecipeChanges recipe)
    {
        if (recipe is null) return BadRequest("Recipe data is required");

        await _recipeService.PatchRecipe(id, recipe);
        return Ok();
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteRecipes([FromQuery] string ids)
    {
        if (string.IsNullOrEmpty(ids)) return BadRequest("No recipe IDs provided");
        
        var deletedCount = await _recipeService.DeleteRecipesByIds(ids);

        if (deletedCount == 0) return NotFound("No recipes found with the provided IDs");
        
        return NoContent();
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomRecipes()
    {
        var recipes = await _recipeService.GetRandomRecipes();
        return Ok(recipes);
    }
}
