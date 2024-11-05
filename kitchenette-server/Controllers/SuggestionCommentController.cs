using kitchenette_server.Interfaces.SuggestionComments;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/suggestionComments")]
public class SuggestionCommentController : ControllerBase
{
    private readonly ISuggestionCommentService _commentService;

    public SuggestionCommentController(ISuggestionCommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetCommentsBySuggestionId([FromQuery] int suggestionId)
    {
        var comments = await _commentService.GetCommentsBySuggestionId(suggestionId);
        return Ok(comments);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> AddSuggestionComment(SuggestionComment recipeReview)
    {
        var comment = await _commentService.AddSuggestionComment(recipeReview);
        return Ok(comment);
    }
}