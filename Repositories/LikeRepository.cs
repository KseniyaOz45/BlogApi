using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<Like?> GetLikeByUserAndPostAsync(string userLogin, string postSlug)
        {
            return await _dbSet
                .Include(l => l.User)
                .Include(l => l.Post)
                .FirstOrDefaultAsync(l => l.User.UserName == userLogin && l.Post.Slug == postSlug);
        }

        public async Task<IEnumerable<Like>> GetLikesByPostAsync(string postSlug)
        {
            return await _dbSet
                .Include(l => l.User)
                .Include(l => l.Post)
                .Where(l => l.Post.Slug == postSlug)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Like>> GetAllAsync()
        {
            return await _dbSet
                .Include(l => l.User)
                .Include(l => l.Post)
                .ToListAsync();
        }

        public override async Task<Like?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(l => l.User)
                .Include(l => l.Post)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
