using Dapper;
using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.SuggestionComments;
using kitchenette_server.Models;

namespace kitchenette_server.Repositories;

public class SuggestionCommentRepository : ISuggestionCommentRepository
{
    private readonly IDbContext _context;
    
    public SuggestionCommentRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<SuggestionComment>> GetCommentsBySuggestionId(int id)
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT
                        SC.Id,
                        SC.SuggestionId,
                        SC.Content,
                        U.Nickname AS CreatedBy,
                        U.Picture AS UserPicture,
                        SC.CreatedAt
                     FROM SuggestionComment SC
                     INNER JOIN Users U ON SC.CreatedBy = U.Id
                     WHERE SC.SuggestionId = @id; ";
        
        var comments = await connection.QueryAsync<SuggestionComment>(sql, new { id });
        return comments;
    }
    
    public async Task<SuggestionComment> AddSuggestionComment(SuggestionComment comment)
    {
        using var connection = _context.CreateConnection();

        var sql = @" INSERT INTO SuggestionComment (SuggestionId, Content, CreatedBy, CreatedAt)
                     OUTPUT INSERTED.Id
                     VALUES (@SuggestionId, @Content, @CreatedBy, @CreatedAt) ";
        
        var now = DateTime.UtcNow;
        var id = await connection.ExecuteScalarAsync<int>(sql, new
        {
            comment.SuggestionId,
            comment.Content,
            comment.CreatedBy,
            CreatedAt = now
        });
        
        comment.Id = id;
        comment.CreatedAt = now;

        return comment;
    }
}