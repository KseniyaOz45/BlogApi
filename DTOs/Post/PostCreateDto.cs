using BlogApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Post
{
    public class PostCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Title cannot be only whitespace")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        [StringLength(5000)]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Title cannot be only whitespace")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }

        public bool IsPublished { get; set; } = false;
    }
}
