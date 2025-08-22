using BlogApi.DTOs.PostReport;

namespace BlogApi.Services.Interfaces
{
    public interface IPostReportService
    {
        Task<IEnumerable<PostReportResponseDto>> GetAllPostReports();
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByPostId(int postId);
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserId(int userId);
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByReasonId(int reasonId);
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByDate(DateTime date);
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserAndDate(int userId, DateTime date);
        Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserAndPostAndDate(int userId, int postId, DateTime date);
        Task<PostReportResponseDto> CreatePostReport(int userId, PostReportCreateDto postReportCreateDto);
        Task<bool> DeletePostReport(int postReportId);
    }
}
