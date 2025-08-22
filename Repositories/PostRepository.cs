using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
        }

        public async Task<Post?> GetPostBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryAsync(string categoryName)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.Category.Name == categoryName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTagAsync(string tagName)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.Tags.Any(t => t.Name == tagName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAndTypeAsync(string userLogin, bool isPublished)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.User.UserName == userLogin && p.IsPublished == isPublished)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAndValueAsync(string userLogin, string searchValue)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.User.UserName == userLogin && (p.Title.Contains(searchValue) || p.Content.Contains(searchValue)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAsync(string userLogin)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.User.UserName == userLogin)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync(int count)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsByValueAsync(string searchValue)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.Title.Contains(searchValue) || p.Content.Contains(searchValue))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ToListAsync();
        }

        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByTagsAsync(List<int> tagIds)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Where(p => p.Tags.Any(t => tagIds.Contains(t.Id)))
                .ToListAsync();
        }
    }
}
