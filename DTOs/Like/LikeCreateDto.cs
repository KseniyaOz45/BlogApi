using BlogApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Like
{
    public class LikeCreateDto
    {
        [Required(ErrorMessage = "Post is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid post")]
        public int PostId { get; set; }
    }
}
