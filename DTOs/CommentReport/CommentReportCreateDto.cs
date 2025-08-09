using BlogApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.CommentReport
{
    public class CommentReportCreateDto
    {
        [Required(ErrorMessage = "Comment is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid comment")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid reason")]
        public int ReasonId { get; set; }

        [StringLength(500)]
        [MinLength(2, ErrorMessage = "Content must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Content cannot be only whitespace")]
        public string? Content { get; set; }
    }
}
