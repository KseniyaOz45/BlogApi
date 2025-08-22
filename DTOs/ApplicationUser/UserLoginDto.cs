using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.ApplicationUser
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
