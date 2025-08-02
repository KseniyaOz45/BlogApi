namespace BlogApi.Models
{
    public class CommentReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int ReasonId { get; set; }
        public Reason Reason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Content { get; set; } = string.Empty;
    }
}