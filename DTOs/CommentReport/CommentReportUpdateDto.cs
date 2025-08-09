using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.CommentReport
{
    public class CommentReportUpdateDto
    {
        [StringLength(500)]
        [MinLength(2, ErrorMessage = "Content must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Content cannot be only whitespace")]
        public string? Content { get; set; }
    }
}
