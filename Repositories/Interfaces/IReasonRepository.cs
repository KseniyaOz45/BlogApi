using BlogApi.Models;

namespace BlogApi.Repositories.Interfaces
{
    public interface IReasonRepository : IGenericRepository<Reason>
    {
        Task<Reason?> GetReasonByNameAsync(string name);
        Task<Reason?> GetReasonBySlugAsync(string slug);
    }
}
