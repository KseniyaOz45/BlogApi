using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task <Like?> GetLikeByUserAndPostAsync(string userLogin, string postSlug);
        Task<IEnumerable<Like>> GetLikesByPostAsync(string postSlug);
    }
}
