using BlogApi.DTOs.Comment;
using BlogApi.DTOs.CommentReport;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentReportsController : ControllerBase
    {
        private readonly ICommentReportService _commentReportService;

        public CommentReportsController(ICommentReportService commentReportService)
        {
            _commentReportService = commentReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentReports() {
            var commentReports = await _commentReportService.GetAllReportsAsync();
            return Ok(commentReports);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentReportById(int id) {
            var commentReport = await _commentReportService.GetCommentReportByIdAsync(id);
            return Ok(commentReport);
        }

        [HttpGet("by-comment/{commentId:int}")]
        public async Task<IActionResult> GetCommentReportsByCommentId(int commentId) {
            var commentReport = await _commentReportService.GetAllReportsByCommentAsync(commentId);
            return Ok(commentReport);
        }

        [HttpGet("by-date/{date:datetime}")]
        public async Task<IActionResult> GetCommentReportsByDateTime(DateTime date) {
            var commentReports = await _commentReportService.GetAllReportsByDateAsync(date);
            return Ok(commentReports);
        }

        [HttpGet("by-user/{userLogin}")]
        public async Task<IActionResult> GetCommentReportsByUserLogin(string userLogin) {
            var commentReports = await _commentReportService.GetAllReportsByUserAsync(userLogin);
            return Ok(commentReports);
        }

        [HttpGet("by-reason/{reasonId:int}")]
        public async Task<IActionResult> GetCommentReportsByReasonId(int reasonId) {
            var commentReports = await _commentReportService.GetAllReportsByReasonAsync(reasonId);
            return Ok(commentReports);
        }

        [HttpGet("by-date/{date:datetime}/by-user/{userLogin}")]
        public async Task<IActionResult> GetCommentReportsByDateAndUserLogin(DateTime date, string userLogin) {
            var commentReports = await _commentReportService.GetAllReportsByUserAndDateAsync(date, userLogin);
            return Ok(commentReports);
        }

        [HttpGet("by-date/{date:datetime}/by-user/{userId:int}/by-comment/{commentId:int}")]
        public async Task<IActionResult> GetCommentReportsByUserIdAndDateAndCommentId(DateTime date, int userId, int commentId) {
            var commentReports = await _commentReportService.GetAllReportsByUserAndDateAndCommentAsync(date, userId, commentId);
            return Ok(commentReports);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentReport([FromBody] CommentReportCreateDto commentReportCreateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _commentReportService.CreateReportAsync(int.Parse(userId), commentReportCreateDto);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCommentReport(int id) {
            var result = await _commentReportService.DeleteReportAsync(id);
            return Ok(result);
        }
    }
}
