using BlogApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReport> CommentReports { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составной ключ для PostTag

            modelBuilder.Entity<Like>()
                .HasIndex(l => new { l.UserId, l.PostId })
                .IsUnique();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostReport>()
                .HasOne(pr => pr.User)
                .WithMany(u => u.PostReports)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostReport>()
                .HasOne(pr => pr.Post)
                .WithMany(p => p.PostReports)
                .HasForeignKey(pr => pr.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentReport>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.CommentReports)
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentReport>()
                .HasOne(cr => cr.Comment)
                .WithMany(c => c.CommentReports)
                .HasForeignKey(cr => cr.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
