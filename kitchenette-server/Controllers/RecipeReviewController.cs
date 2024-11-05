using kitchenette_server.Interfaces.RecipeReviews;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/recipeReviews")]
public class RecipeReviewController : ControllerBase
{
    private readonly IRecipeReviewService _recipeReviewService;
    
    public RecipeReviewController(IRecipeReviewService recipeReviewService)
    {
        _recipeReviewService = recipeReviewService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetRecipeReviewsByRecipeId([FromQuery] int recipeId)
    {
        var reviews = await _recipeReviewService.GetRecipeReviewsByRecipeId(recipeId);
        return Ok(reviews);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> AddRecipeReview(RecipeReview recipeReview)
    {
        var review = await _recipeReviewService.AddRecipeReview(recipeReview);
        return Ok(review);
    }

    [HttpGet("{id}/average-rating")]
    public async Task<IActionResult> GetAverageRecipeRating(int id)
    {
        var rating = await _recipeReviewService.GetAverageRecipeRating(id);
        return Ok(rating);
    }
}