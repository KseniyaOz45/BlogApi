using Microsoft.AspNetCore.Identity;

namespace BlogApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Slug { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<PostReport> PostReports { get; set; } = new List<PostReport>();
        public ICollection<CommentReport> CommentReports { get; set; } = new List<CommentReport>();
    }
}
