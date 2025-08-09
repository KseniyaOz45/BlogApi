using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Tag
{
    public class TagCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Name cannot be only whitespace")]
        public string Name { get; set; } = string.Empty;
    }
}
