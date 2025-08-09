using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Post
{
    public class PostUpdateDto
    {
        [StringLength(100)]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Title cannot be only whitespace")]
        public string? Title { get; set; }

        [StringLength(5000)]
        [RegularExpression(@"\S+", ErrorMessage = "Content cannot be only whitespace")]
        public string? Content { get; set; }

        public IFormFile? Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int? CategoryId { get; set; }

        public bool? IsPublished { get; set; }
    }
}
