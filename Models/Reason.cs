namespace BlogApi.Models
{
    public class Reason
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<CommentReport> CommentReports { get; set; } = new List<CommentReport>();
        public ICollection<PostReport> PostReports { get; set; } = new List<PostReport>();
    }
}