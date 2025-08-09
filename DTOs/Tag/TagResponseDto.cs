namespace BlogApi.DTOs.Tag
{
    public class TagResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<string> PostTitles { get; set; } = new List<string>();
    }
}
