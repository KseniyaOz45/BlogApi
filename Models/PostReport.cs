namespace BlogApi.Models
{
    public class PostReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int ReasonId { get; set; }
        public Reason Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Content { get; set; } = string.Empty;
    }
}