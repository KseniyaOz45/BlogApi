using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [StringLength(500)]
        [MinLength(2, ErrorMessage = "Comment must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Comment cannot be only whitespace")]
        public string? Content { get; set; }
    }
}
