using BlogApi.Models;

namespace BlogApi.DTOs.Post
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public int LikesCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsPublished { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<int> CommentIds { get; set; } = new List<int>();
        public ICollection<string> TagNames { get; set; } = new List<string>();
        public ICollection<int> PostReportsIds { get; set; } = new List<int>();
    }
}
