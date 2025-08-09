using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2)]
        [StringLength(100)]
        [RegularExpression(@"\S+", ErrorMessage = "Name cannot be empty or whitespace")]
        public string Name { get; set; } = string.Empty;
    }
}
