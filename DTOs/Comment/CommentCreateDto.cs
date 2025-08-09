using BlogApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Comment
{
    public class CommentCreateDto
    {
        [Required(ErrorMessage = "Content is required")]
        [StringLength(500)]
        [MinLength(2, ErrorMessage = "Comment must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Comment cannot be only whitespace")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid post")]
        public int PostId { get; set; }
    }
}
