namespace kitchenette_server.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}