using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Reason
{
    public class ReasonUpdateDto
    {
        [StringLength(100)]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Name cannot be only whitespace")]
        public string? Name { get; set; }
    }
}
