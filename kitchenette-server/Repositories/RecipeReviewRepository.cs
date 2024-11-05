using Dapper;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.RecipeReviews;
using kitchenette_server.Models;

namespace kitchenette_server.Repositories;

public class RecipeReviewRepository : IRecipeReviewRepository
{
    private readonly IDbContext _context;

    public RecipeReviewRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecipeReview>> GetRecipeReviewsByRecipeId(int id)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT
                        RR.Id,
                        RR.RecipeId,
                        RR.Rating,
                        RR.Content,
                        U.Nickname AS CreatedBy,
                        U.Picture AS UserPicture,
                        RR.CreatedAt
                     FROM RecipeReview RR
                     INNER JOIN Users U ON RR.CreatedBy = U.Id
                     WHERE RR.RecipeId = @id; ";
        
        var reviews = await connection.QueryAsync<RecipeReview>(sql, new { id });
        return reviews;
    }
    
    public async Task<RecipeReview> AddRecipeReview(RecipeReview recipeReview)
    {
        using var connection = _context.CreateConnection();

        var sql = @" INSERT INTO RecipeReview (
                        RecipeId,
                        Rating,
                        Content,
                        CreatedBy,
                        CreatedAt )
                     VALUES (
                        @RecipeId,
                        @Rating,
                        @Content,
                        @CreatedBy,
                        @CreatedAt ) RETURNING Id ";
        
        var now = DateTime.UtcNow;
        var id = await connection.ExecuteScalarAsync<int>(sql, new
        {
            recipeReview.RecipeId,
            recipeReview.Rating,
            recipeReview.Content,
            recipeReview.CreatedBy,
            CreatedAt = now
        });
        
        recipeReview.Id = id;
        recipeReview.CreatedAt = now;

        return recipeReview;
    }

    public async Task<int> GetAverageRecipeRating(int id)
    {
        using var connection = _context.CreateConnection();
    
        var sql = @" SELECT ROUND(AVG(Rating)) AS AverageRating
                     FROM RecipeReview
                     WHERE RecipeId = @id; ";
        
        var rating  = await connection.QuerySingleAsync<int>(sql, new { id });
        return rating;
    }
}