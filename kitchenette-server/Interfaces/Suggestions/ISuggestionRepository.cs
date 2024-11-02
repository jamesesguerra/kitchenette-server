using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Suggestions;

public interface ISuggestionRepository
{
    Task<SuggestionDto> GetSuggestionByIdWithChanges(int id);
    Task<SuggestionDto> AddSuggestion(SuggestionDto suggestion);
    Task<IEnumerable<SuggestionDto>> GetSuggestionsByRecipeId(int id);
    Task PatchSuggestion(int id, Suggestion suggestion);
}