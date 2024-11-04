namespace kitchenette_server.Models;

public class RecipeReview
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public short Rating { get; set; }
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public string? UserPicture { get; set; }
    public DateTime CreatedAt { get; set; }
}