using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(string categoryName);
        Task<IEnumerable<Post>> GetPostsByUserAsync(string userLogin);
        Task<IEnumerable<Post>> GetPostsByTagAsync(string tagName);
        Task<IEnumerable<Post>> GetPostsByTagsAsync(List<int> tagIds);
        Task<IEnumerable<Post>> GetRecentPostsAsync(int count);
        Task<IEnumerable<Post>> SearchPostsByValueAsync(string searchValue);
        Task<IEnumerable<Post>> GetPostsByUserAndValueAsync(string userLogin, string searchValue);
        Task<IEnumerable<Post>> GetPostsByUserAndTypeAsync(string userLogin, bool isPublished);
        Task<Post?> GetPostBySlugAsync(string slug);
    }
}
