using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<Tag?> GetTagByNameAsync(string tagName)
        {
            return await _dbSet
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Name.ToLower() == tagName.ToLower());
        }

        public async Task<Tag?> GetTagBySlugAsync(string tagSlug)
        {
            return await _dbSet
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Slug.ToLower() == tagSlug.ToLower());
        }
    }
}
