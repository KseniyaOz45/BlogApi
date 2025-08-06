using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface ICommentReportRepository : IGenericRepository<CommentReport>
    {
        Task<IEnumerable<CommentReport>> GetReportsByCommentIdAsync(int commentId);
        Task<IEnumerable<CommentReport>> GetReportsByUserLoginAsync(string userLogin);
        Task<IEnumerable<CommentReport>> GetReportsByReasonSlugAsync(string reasonSlug);
        Task<CommentReport?> GetReportByCommentAndUserAsync(int commentId, string userLogin);
    }
}
