using kitchenette_server.Models;

namespace kitchenette_server.Dtos;

public class SuggestionDto
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } = "Open";
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public RecipeChanges RecipeChanges { get; set; }
}