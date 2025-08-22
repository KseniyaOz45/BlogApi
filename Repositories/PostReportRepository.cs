using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class PostReportRepository : GenericRepository<PostReport>, IPostReportRepository
    {
        public PostReportRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PostReport>> GetReportsByPostSlugAsync(string postSlug)
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .Where(report => report.Post.Slug == postSlug)
                .ToListAsync();
        }
        public async Task<IEnumerable<PostReport>> GetReportsByUserLoginAsync(string userLogin)
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .Where(report => report.User.UserName == userLogin)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostReport>> GetReportsByReasonSlugAsync(string reasonSlug)
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .Where(report => report.Reason.Slug == reasonSlug)
                .ToListAsync();
        }

        public async Task<PostReport?> GetReportByPostSlugAndUserLoginAsync(string postSlug, string userLogin)
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .FirstOrDefaultAsync(report => report.Post.Slug == postSlug && report.User.UserName == userLogin);
        }

        public override async Task<IEnumerable<PostReport>> GetAllAsync()
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .ToListAsync();
        }

        public override async Task<PostReport?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(pr => pr.Post)
                .Include(pr => pr.User)
                .Include(pr => pr.Reason)
                .FirstOrDefaultAsync(report => report.Id == id);
        }
    }
}
