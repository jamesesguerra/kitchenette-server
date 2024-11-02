using kitchenette_server.Dtos;
using kitchenette_server.Interfaces.Suggestions;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/suggestions")]
public class SuggestionController : ControllerBase
{
    private readonly ISuggestionService _suggestionService;
    
    public SuggestionController(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSuggestionByIdWithChanges(int id)
    {
        var suggestion = await _suggestionService.GetSuggestionByIdWithChanges(id);
        return Ok(suggestion);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddSuggestion([FromBody] SuggestionDto suggestion)
    {
        var newSuggestion = await _suggestionService.AddSuggestion(suggestion);
        return Ok(newSuggestion);
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetSuggestionsByRecipeId([FromQuery] int recipeId)
    {
        var suggestions = await _suggestionService.GetSuggestionsByRecipeId(recipeId);
        return Ok(suggestions);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchSuggestion(int id, Suggestion suggestion)
    {
        await _suggestionService.PatchSuggestion(id, suggestion);
        return Ok();
    }
}