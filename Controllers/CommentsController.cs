using BlogApi.DTOs.Comment;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommets() {
            var comments = await _commentService.GetAllComments();
            return Ok(comments);
        }

        [HttpGet("by-post/{postId:int}")]
        public async Task<IActionResult> GetCommentsByPostId(int postId) {
            var comments = await _commentService.GetAllCommentsToPost(postId);
            return Ok(comments);
        }

        [HttpGet("by-user/{userId:int}")]
        public async Task<IActionResult> GetCommentsByUserId(int userId) {
            var comments = await _commentService.GetAllCommentsByUser(userId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] CommentCreateDto commentCreateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _commentService.CreateComment(int.Parse(userId), commentCreateDto);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id) {
            var result = await _commentService.DeleteComment(id);
            return Ok(result);
        }
    }
}
