using kitchenette_server.Context;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Interfaces.Users;
using kitchenette_server.Interfaces.Collections;
using kitchenette_server.Interfaces.RecipeReviews;
using kitchenette_server.Interfaces.SuggestionComments;
using kitchenette_server.Interfaces.Suggestions;
using kitchenette_server.Repositories;
using kitchenette_server.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage"));
});

// Add services to the container.
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
builder.Services.AddSingleton<IRecipeService, RecipeService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICollectionRepository, CollectionRepository>();
builder.Services.AddSingleton<ICollectionService, CollectionService>();
builder.Services.AddSingleton<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddSingleton<ISuggestionService, SuggestionService>();
builder.Services.AddSingleton<IRecipeReviewRepository, RecipeReviewRepository>();
builder.Services.AddSingleton<IRecipeReviewService, RecipeReviewService>();
builder.Services.AddSingleton<ISuggestionCommentRepository, SuggestionCommentRepository>();
builder.Services.AddSingleton<ISuggestionCommentService, SuggestionCommentService>();

var allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:4200" }
    : new[] { "https://kitchenet.vercel.app" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", 
        policy => policy
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
