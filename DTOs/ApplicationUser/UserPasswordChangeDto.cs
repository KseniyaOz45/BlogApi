using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.ApplicationUser
{
    public class UserPasswordChangeDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
