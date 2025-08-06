using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogDbContext context) : base(context)
        {
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _dbSet.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<Category?> GetCategoryBySlugAsync(string slug)
        {
            return await _dbSet.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Slug == slug);
        }
        
        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Posts).ToListAsync();
        }
        public override async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
