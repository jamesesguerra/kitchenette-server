namespace kitchenette_server.Models;

public class Recipe
{
    public int ID { get; set; }
    public int CollectionID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CoverImageURL { get; set; } = string.Empty;
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public string Ingredients { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int NutritionFactsID { get; set; }
}