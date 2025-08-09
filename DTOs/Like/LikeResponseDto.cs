using BlogApi.Models;

namespace BlogApi.DTOs.Like
{
    public class LikeResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
    }
}
