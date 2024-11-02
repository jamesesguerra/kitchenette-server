namespace kitchenette_server.Models;

public class RecipeChanges
{
    public int Id { get; set; }
    public int SuggestionId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? CoverPicture { get; set; }
    public int? PrepTime { get; set; }
    public int? CookTime { get; set; }
    public string? Ingredients { get; set; }
    public string? Instructions { get; set; }
    public int? Servings { get; set; }
    public int? Calories { get; set; }
    public int? Protein { get; set; }
    public int? Fat { get; set; }
    public int? Fiber { get; set;  }
    public int? Carbohydrates { get; set; }
}