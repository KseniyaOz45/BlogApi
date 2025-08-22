using BlogApi.DTOs.CommentReport;

namespace BlogApi.Services.Interfaces
{
    public interface ICommentReportService
    {
        Task<CommentReportResponseDto> GetCommentReportByIdAsync(int id);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByCommentAsync(int commentId);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByDateAsync(DateTime dateTime);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAsync(string userLogin);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByReasonAsync(int reasonId);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAndDateAsync(DateTime dateTime, string userLogin);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAndDateAndCommentAsync(DateTime dateTime, int userId ,int commentId);
        Task<IEnumerable<CommentReportResponseDto>> GetAllReportsAsync();
        Task<CommentReportResponseDto> CreateReportAsync(int userId, CommentReportCreateDto reportCreateDto);
        Task<bool> DeleteReportAsync(int reportId);

    }
}
