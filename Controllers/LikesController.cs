using BlogApi.DTOs.Like;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService) {
            _likeService = likeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLikes() {
            var likes = await _likeService.GetAllLikes();
            return Ok(likes);
        }

        [HttpGet("by-post/{postSlug:string}")]
        public async Task<IActionResult> GetLikesByPostSlug(string postSlug) {
            var likes = await _likeService.GetLikesByPostSlug(postSlug);
            return Ok(likes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLikeByPost(int id) {
            var like = await _likeService.GetLikeById(id);
            return Ok(like);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLike([FromBody] LikeCreateDto likeCreateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _likeService.CreateLike(int.Parse(userId), likeCreateDto);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLike(int id) {
            var result = await _likeService.DeleteLike(id);
            return Ok(result);
        }
    }
}
