using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Interfaces.Users;
using kitchenette_server.Interfaces.Collections;
using kitchenette_server.Interfaces.RecipeReviews;
using kitchenette_server.Interfaces.SuggestionComments;
using kitchenette_server.Interfaces.Suggestions;
using kitchenette_server.Repositories;
using kitchenette_server.Services;
using kitchenette_server.Context;

namespace kitchenette_server.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDbContext, DbContext>();
        services.AddSingleton<IRecipeRepository, RecipeRepository>();
        services.AddSingleton<IRecipeService, RecipeService>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ICollectionRepository, CollectionRepository>();
        services.AddSingleton<ICollectionService, CollectionService>();
        services.AddSingleton<ISuggestionRepository, SuggestionRepository>();
        services.AddSingleton<ISuggestionService, SuggestionService>();
        services.AddSingleton<IRecipeReviewRepository, RecipeReviewRepository>();
        services.AddSingleton<IRecipeReviewService, RecipeReviewService>();
        services.AddSingleton<ISuggestionCommentRepository, SuggestionCommentRepository>();
        services.AddSingleton<ISuggestionCommentService, SuggestionCommentService>();
        return services;
    }
}