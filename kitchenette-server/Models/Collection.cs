namespace kitchenette_server.Models;

public class Collection
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public DateTime CreatedDate { get; set; }
}