using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Category
{
    public class CategoryUpdateDto
    {
        [MinLength(2)]
        [StringLength(100)]
        [RegularExpression(@"\S+", ErrorMessage = "Name cannot be empty or whitespace")]
        public string? Name { get; set; }
    }
}
