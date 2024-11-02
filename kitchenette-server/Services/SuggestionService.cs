using kitchenette_server.Dtos;
using kitchenette_server.Interfaces.Suggestions;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class SuggestionService : ISuggestionService
{
    private readonly ISuggestionRepository _suggestionRepository;

    public SuggestionService(ISuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public async Task<SuggestionDto> GetSuggestionByIdWithChanges(int id)
    {
        return await _suggestionRepository.GetSuggestionByIdWithChanges(id);
    }
    
    public async Task<SuggestionDto> AddSuggestion(SuggestionDto suggestion)
    {
        return await _suggestionRepository.AddSuggestion(suggestion);
    }

    public async Task<IEnumerable<SuggestionDto>> GetSuggestionsByRecipeId(int id)
    {
        return await _suggestionRepository.GetSuggestionsByRecipeId(id);
    }

    public async Task PatchSuggestion(int id, Suggestion suggestion)
    {
        await _suggestionRepository.PatchSuggestion(id, suggestion);
    }
}