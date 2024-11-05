using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.SuggestionComments;

public interface ISuggestionCommentRepository
{
    Task<IEnumerable<SuggestionComment>> GetCommentsBySuggestionId(int id);
    Task<SuggestionComment> AddSuggestionComment(SuggestionComment comment);
}