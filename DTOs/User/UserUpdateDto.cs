using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.User
{
    public class UserUpdateDto
    {
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "Login must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Login cannot be only whitespace")]
        public string? Login { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Avatar { get; set; }
    }
}
