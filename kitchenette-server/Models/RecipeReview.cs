namespace kitchenette_server.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public string? UserPicture { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RecipeReview : Comment
{
    public int RecipeId { get; set; }
    public short Rating { get; set; }
}

public class SuggestionComment : Comment
{
    public int SuggestionId { get; set; }
}