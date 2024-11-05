using kitchenette_server.Interfaces.SuggestionComments;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class SuggestionCommentService : ISuggestionCommentService
{
    private readonly ISuggestionCommentRepository _commentRepository;
    
    public SuggestionCommentService(ISuggestionCommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public Task<IEnumerable<SuggestionComment>> GetCommentsBySuggestionId(int id)
    {
       return  _commentRepository.GetCommentsBySuggestionId(id);
    }

    public Task<SuggestionComment> AddSuggestionComment(SuggestionComment comment)
    {
        return _commentRepository.AddSuggestionComment(comment);
    }
}