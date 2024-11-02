namespace kitchenette_server.Dtos;

public class RecipeSummaryDto
{
    public int Id { get; set; }
    public string Collection  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverPicture { get; set; }
    public DateTime CreatedAt { get; set; }
}