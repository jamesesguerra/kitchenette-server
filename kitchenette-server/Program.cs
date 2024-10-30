using kitchenette_server.Context;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Recipes;
using kitchenette_server.Interfaces.Users;
using kitchenette_server.Interfaces.Collections;
using kitchenette_server.Repositories;
using kitchenette_server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
builder.Services.AddSingleton<IRecipeService, RecipeService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICollectionRepository, CollectionRepository>();
builder.Services.AddSingleton<ICollectionService, CollectionService>();

builder.WebHost.UseUrls("http://*:5280");

var allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:4200" }
    : new[] { "https://kitchenette-ui.vercel.app", "https://kitchenetteapp.netlify.app" };

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
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
