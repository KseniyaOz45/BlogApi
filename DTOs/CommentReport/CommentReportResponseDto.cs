using BlogApi.Models;

namespace BlogApi.DTOs.CommentReport
{
    public class CommentReportResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int CommentId { get; set; }

        public int ReasonId { get; set; }
        public string ReasonName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
    }
}
