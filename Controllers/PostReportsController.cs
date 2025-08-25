using BlogApi.DTOs.PostReport;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReportsController : ControllerBase
    {
        private readonly IPostReportService _postReportService;

        public PostReportsController(IPostReportService postReportService)
        {
            _postReportService = postReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostReports() {
            var postReports = await _postReportService.GetAllPostReports();
            return Ok(postReports);
        }

        [HttpGet("by-post/{postId:int}")]
        public async Task<IActionResult> GetPostReportsByPostId(int postId) {
            var postReports = await _postReportService.GetPostReportsByPostId(postId);
            return Ok(postReports);
        }

        [HttpGet("by-user/{userId:int}")]
        public async Task<IActionResult> GetPostReportsByUserId(int userId) {
            var postReports = await _postReportService.GetPostReportsByUserId(userId);
            return Ok(postReports);
        }

        [HttpGet("by-reason/{reasonId:int}")]
        public async Task<IActionResult> GetPostReportsByReasonId(int reasonId) {
            var postReports = await _postReportService.GetPostReportsByReasonId(reasonId);
            return Ok(postReports);
        }

        [HttpGet("by-date/{date:datetime}")]
        public async Task<IActionResult> GetPostReportsByDate(DateTime date) {
            var postReports = await _postReportService.GetPostReportsByDate(date);
            return Ok(postReports);
        }

        [HttpGet("by-user/{userId:int}/by-date/{date:datetime}")]
        public async Task<IActionResult> GetPostReportsByUserIdAndDate(int userId, DateTime date) {
            var postReports = await _postReportService.GetPostReportsByUserAndDate(userId, date);
            return Ok(postReports);
        }

        [HttpGet("by-user/{userId:int}/by-post/{postId:int}/by-date/{date:datetime}")]
        public async Task<IActionResult> GetPostReportsByUserIdAndPostIdAndDate(int userId, int postId, DateTime date) {
            var postReports = await _postReportService.GetPostReportsByUserAndPostAndDate(userId, postId, date);
            return Ok(postReports);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostReport([FromBody] PostReportCreateDto postReportCreateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _postReportService.CreatePostReport(int.Parse(userId), postReportCreateDto);
            return CreatedAtAction(nameof(GetAllPostReports), new { id = result.Id }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePostReport(int id) {
            var result = await _postReportService.DeletePostReport(id);
            return Ok(result);
        }
    }
}
