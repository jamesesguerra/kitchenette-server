using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.SuggestionComments;

public interface ISuggestionCommentService
{
    Task<IEnumerable<SuggestionComment>> GetCommentsBySuggestionId(int id);
    Task<SuggestionComment> AddSuggestionComment(SuggestionComment comment);
}