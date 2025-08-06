using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<Category?> GetCategoryBySlugAsync(string slug);
    }
}
