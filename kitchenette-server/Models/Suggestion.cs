namespace kitchenette_server.Models;

public class Suggestion
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}