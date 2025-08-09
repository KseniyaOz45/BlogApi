namespace BlogApi.DTOs.Reason
{
    public class ReasonResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<int> CommentReportIds { get; set; } = new List<int>();
        public ICollection<int> PostReportIds { get; set; } = new List<int>();
    }
}
