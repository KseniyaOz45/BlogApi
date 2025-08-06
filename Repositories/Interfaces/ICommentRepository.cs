using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface ICommentRepository: IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostSlugAsync(string postSlug);
        Task<IEnumerable<Comment>> GetCommentsByUserLoginAsync(string userLogin);
    }
}
