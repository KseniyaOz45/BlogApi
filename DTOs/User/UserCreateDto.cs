using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "Login must be at least 2 characters long")]
        [RegularExpression(@"\S+", ErrorMessage = "Login cannot be only whitespace")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Upload)]
        public IFormFile? Avatar { get; set; }
    }
}
