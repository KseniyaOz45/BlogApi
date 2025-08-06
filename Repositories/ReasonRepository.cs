using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class ReasonRepository : GenericRepository<Reason>, IReasonRepository
    {
        public ReasonRepository(BlogDbContext context) : base(context)
        {
        }
        public async Task<Reason?> GetReasonByNameAsync(string name)
        {
            return await _dbSet
                .Include(r => r.PostReports)
                .Include(r => r.CommentReports)
                .FirstOrDefaultAsync(r => r.Name == name);
        }
        public async Task<Reason?> GetReasonBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(r => r.PostReports)
                .Include(r => r.CommentReports)
                .FirstOrDefaultAsync(r => r.Slug == slug);
        }
        public async Task<List<Reason>> GetAllReasonsAsync()
        {
            return await _dbSet
                .Include(r => r.PostReports)
                .Include(r => r.CommentReports)
                .ToListAsync();
        }
        public async Task<Reason?> GetReasonByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.PostReports)
                .Include(r => r.CommentReports)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
