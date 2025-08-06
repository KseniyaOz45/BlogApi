using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface IPostReportRepository : IGenericRepository<PostReport>
    {
        Task<IEnumerable<PostReport>> GetReportsByPostSlugAsync(string postSlug);
        Task<IEnumerable<PostReport>> GetReportsByUserLoginAsync(string userLogin);
        Task<IEnumerable<PostReport>> GetReportsByReasonSlugAsync(string reasonSlug);
        Task<PostReport?> GetReportByPostSlugAndUserLoginAsync(string postSlug, string userLogin);
    }
}
