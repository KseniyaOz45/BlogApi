using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPostSlugAsync(string postSlug)
        {
            return await _dbSet
                .Include(c => c.Post)
                .Include(c => c.User)
                .Where(c => c.Post.Slug == postSlug)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByUserLoginAsync(string userLogin)
        {
            return await _dbSet
                .Include(c => c.Post)
                .Include(c => c.User)
                .Where(c => c.User.Login == userLogin)
                .ToListAsync();
        }
        
        public override async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Post).Include(c => c.User).ToListAsync();
        }
        
        public override async Task<Comment?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.Post).Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
