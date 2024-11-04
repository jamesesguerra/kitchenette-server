using kitchenette_server.Interfaces.RecipeReviews;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class RecipeReviewService : IRecipeReviewService
{
    private readonly IRecipeReviewRepository _recipeReviewRepository;

    public RecipeReviewService(IRecipeReviewRepository recipeReviewRepository)
    {
        _recipeReviewRepository = recipeReviewRepository;
    }

    public async Task<IEnumerable<RecipeReview>> GetRecipeReviewsByRecipeId(int id)
    {
        return await _recipeReviewRepository.GetRecipeReviewsByRecipeId(id);
    }
    
    public async Task<RecipeReview> AddRecipeReview(RecipeReview recipeReview)
    {
        return await _recipeReviewRepository.AddRecipeReview(recipeReview);
    }
}