using BlogApi.Models;

namespace BlogApi.DTOs.Comment
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;

        public int CommentReportsCount { get; set; }
        public IEnumerable<int> CommentReportsIds { get; set; } = new List<int>();
    }
}
