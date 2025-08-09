using BlogApi.Models;

namespace BlogApi.DTOs.PostReport
{
    public class PostReportResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public int ReasonId { get; set; }
        public string ReasonName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
    }
}
