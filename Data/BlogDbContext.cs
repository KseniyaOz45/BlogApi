using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReport> CommentReports { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составной ключ для PostTag
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
