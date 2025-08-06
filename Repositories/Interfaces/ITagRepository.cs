using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag?> GetTagByNameAsync(string tagName);
        Task<Tag?> GetTagBySlugAsync(string tagSlug);
    }
}
