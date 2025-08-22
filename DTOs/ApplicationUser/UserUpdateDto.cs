using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.User
{
    public class UserUpdateDto
    {
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "UserName must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "UserName cannot be only whitespace")]
        public string? UserName { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Avatar { get; set; }
    }
}
