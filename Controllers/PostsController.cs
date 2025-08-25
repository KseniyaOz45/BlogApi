using BlogApi.DTOs.Post;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService) {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts() {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("most-popular")]
        public async Task<IActionResult> GetMostPopularPosts() {
            var posts = await _postService.GetMostPopularPosts();
            return Ok(posts);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestPosts() {
            var posts = await _postService.GetLatestPosts();
            return Ok(posts);
        }

        [HttpGet("by-user-name/{userName}")]
        public async Task<IActionResult> GetPostsByUserUsename(string userName) {
            var posts = await _postService.GetPostsByUserName(userName);
            return Ok(posts);
        }

        [HttpGet("by-user-id/{userId:int}")]
        public async Task<IActionResult> GetPostsByUserId(int userId) {
            var posts = await _postService.GetPostsByUserId(userId);
            return Ok(posts);
        }

        [HttpGet("by-user-name/{userName}/drafts")]
        public async Task<IActionResult> GetPostsDraftsByUserName(string userName) {
            var posts = await _postService.GetPostsDraftsByUserName(userName);
            return Ok(posts);
        }

        [HttpGet("by-user-id/{userId:int}/drafts")]
        public async Task<IActionResult> GetPostsDraftsByUserId(int userId) {
            var posts = await _postService.GetPostsDraftsByUserId(userId);
            return Ok(posts);
        }

        [HttpGet("by-category-id/{categoryId:int}")]
        public async Task<IActionResult> GetPostsByCategoryId(int categoryId) {
            var posts = await _postService.GetPostsByCategoryId(categoryId);
            return Ok(posts);
        }

        [HttpGet("by-category-name/{categoryName}")]
        public async Task<IActionResult> GetPostsByCategoryName([FromQuery] string categoryName) {
            var posts = await _postService.GetPostsByCategoryName(categoryName);
            return Ok(posts);
        }

        [HttpGet("by-tags")]
        public async Task<IActionResult> GetPostsByTagsIds([FromQuery] List<int> tagIds) {
            var posts = await _postService.GetPostsByTags(tagIds);
            return Ok(posts);
        }

        [HttpGet("by-title/{title}")]
        public async Task<IActionResult> GetPostsByTitle([FromQuery] string title) {
            var posts = await _postService.GetPostsByTitle(title);
            return Ok(posts);
        }

        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetPostById(int postId) {
            var post = await _postService.GetPostById(postId);
            return Ok(post);
        }

        [HttpGet("by-slug/{postSlug}")]
        public async Task<IActionResult> GetPostBySlug(string postSlug) {
            var post = await _postService.GetPostBySlug(postSlug);
            return Ok(post);
        }

        [HttpPut("{postSlug}/view")]
        public async Task<IActionResult> ViewPost(string postSlug) {
            var post = await _postService.ViewPost(postSlug);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostCreateDto postCreateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _postService.CreatePost(int.Parse(userId), postCreateDto);
            return Ok(result);
        }

        [HttpPut("{postId:int}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromForm] PostUpdateDto postUpdateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _postService.UpdatePost(int.Parse(userId), postId, postUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeletePost(int postId) {
            var result = await _postService.DeletePost(postId);
            return Ok(result);
        }
    }
}
