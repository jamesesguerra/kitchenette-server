using kitchenette_server.Models;

namespace kitchenette_server.Dtos;

public class RecipeDto : Recipe
{
    public string CollectionName { get; set; }
    public string CreatedBy { get; set; }
    public string UserId { get; set; }
    public string UserPicture { get; set; }
}