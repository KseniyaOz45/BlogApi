namespace BlogApi.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<string> PostTitles { get; set; } = new List<string>();
        public ICollection<int> CommentIds { get; set; } = new List<int>();
        public ICollection<int> LikeIds { get; set; } = new List<int>();
        public ICollection<int> PostReportIds { get; set; } = new List<int>();
        public ICollection<int> CommentReportIds { get; set; } = new List<int>();
    }
}
