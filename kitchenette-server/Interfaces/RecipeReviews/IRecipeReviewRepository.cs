using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.RecipeReviews;

public interface IRecipeReviewRepository
{
    Task<IEnumerable<RecipeReview>> GetRecipeReviewsByRecipeId(int id);
    Task<RecipeReview> AddRecipeReview(RecipeReview recipeReview);
}