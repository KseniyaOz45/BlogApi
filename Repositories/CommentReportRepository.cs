using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class CommentReportRepository : GenericRepository<CommentReport>, ICommentReportRepository
    {
        public CommentReportRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CommentReport>> GetReportsByCommentIdAsync(int commentId)
        {
            return await _dbSet.Where(cr => cr.CommentId == commentId)
                .Include(cr => cr.Comment)
                .Include(cr => cr.User)
                .Include(cr => cr.Reason)
                .ToListAsync();
        }

        public async Task<IEnumerable<CommentReport>> GetReportsByUserLoginAsync(string userLogin)
        {
            return await _dbSet
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .Include(cr => cr.Reason)
                .Where(cr => cr.User.UserName == userLogin).ToListAsync();
        }

        public async Task<CommentReport?> GetReportByCommentAndUserAsync(int commentId, string userLogin)
        {
            return await _dbSet
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .Include(cr => cr.Reason)
                .FirstOrDefaultAsync(cr => cr.CommentId == commentId && cr.User.UserName == userLogin);
        }

        public async Task<IEnumerable<CommentReport>> GetReportsByReasonSlugAsync(string reasonSlug)
        {
            return await _dbSet
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .Include(cr => cr.Reason)
                .Where(cr => cr.Reason.Slug == reasonSlug).ToListAsync();
        }

        public override async Task<IEnumerable<CommentReport>> GetAllAsync()
        {
            return await _dbSet
                .Include(cr => cr.Comment)
                .Include(cr => cr.User)
                .Include(cr => cr.Reason)
                .ToListAsync();
        }

        public override async Task<CommentReport?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .Include(cr => cr.Reason)
                .FirstOrDefaultAsync(cr => cr.Id == id);
        }
    }
}
