using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Suggestions;

public interface ISuggestionService
{
    Task<SuggestionDto> GetSuggestionByIdWithChanges(int id);
    Task<SuggestionDto> AddSuggestion(SuggestionDto suggestion);
    Task<IEnumerable<SuggestionDto>> GetSuggestionsByRecipeId(int id);
    Task PatchSuggestion(int id, Suggestion suggestion);
    Task<int> DeleteSuggestion(int id);
}