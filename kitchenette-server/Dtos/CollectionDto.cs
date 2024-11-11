using System.ComponentModel.DataAnnotations;
using kitchenette_server.Models;

namespace kitchenette_server.Dtos;

public class CollectionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public string? UserPicture { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVisible { get; set; }
    public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();
}