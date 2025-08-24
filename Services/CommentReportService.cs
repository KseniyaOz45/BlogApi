using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Category;
using BlogApi.DTOs.CommentReport;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Services
{
    public class CommentReportService : ICommentReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentReportService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CommentReportResponseDto> CreateReportAsync(int userId, CommentReportCreateDto reportCreateDto)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(reportCreateDto.CommentId) 
                ?? throw new ArgumentException($"Comment with id {reportCreateDto.CommentId} does not exists.");
            var user = await _userManager.FindByIdAsync(userId.ToString()) 
                ?? throw new ArgumentException($"User with id {userId} does not exists.");
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reportCreateDto.ReasonId) 
                ?? throw new ArgumentException($"Reason with id {reportCreateDto.ReasonId} does not exists.");

            var newCommentReport = new CommentReport()
            {
                Content = reportCreateDto.Content,
                ReasonId = reportCreateDto.ReasonId,
                CommentId = reportCreateDto.CommentId,
                UserId = user.Id
            };

            await _unitOfWork.CommentReports.AddAsync(newCommentReport);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CommentReportResponseDto>(newCommentReport);
        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            var commentReport = await _unitOfWork.CommentReports.GetByIdAsync(reportId)
                ?? throw new ArgumentException($"Comment report with id {reportId} does not exists."); ;

            _unitOfWork.CommentReports.DeleteAsync(commentReport);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsAsync()
        {
            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(await _unitOfWork.CommentReports.GetAllAsync());
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByCommentAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId)
                ?? throw new ArgumentException($"Comment with id {commentId} does not exists."); ;

            var commentReports = await _unitOfWork.CommentReports.GetReportsByCommentIdAsync(commentId);

            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByDateAsync(DateTime dateTime)
        {
            var commentReports = (await _unitOfWork.CommentReports.GetAllAsync()).Where(cr => cr.CreatedAt >= dateTime);
            if (!commentReports.Any())
            {
                return [];
            }
            else {
                return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
            }
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByReasonAsync(int reasonId)
        {
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reasonId)
                ?? throw new ArgumentException($"Reason with id {reasonId} does not exists.");

            var commentReports = await _unitOfWork.CommentReports.GetReportsByReasonSlugAsync(reason.Slug);
            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAndDateAndCommentAsync(DateTime dateTime, int userId, int commentId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId)
                ?? throw new ArgumentException($"Comment with id {commentId} does not exists.");

            var commentReports = (await _unitOfWork.CommentReports.GetAllAsync()).ToList().Where(cr => 
                cr.UserId == userId && 
                cr.CommentId == commentId && 
                cr.CreatedAt.Date == dateTime.Date);

            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAndDateAsync(DateTime dateTime, string userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin)
                ?? throw new ArgumentException($"User with login {userLogin} does not exists.");

            var commentReports = (await _unitOfWork.CommentReports.GetAllAsync())
                .Where(cr => cr.UserId == user.Id && cr.CreatedAt.Date == dateTime.Date);
            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
        }

        public async Task<IEnumerable<CommentReportResponseDto>> GetAllReportsByUserAsync(string userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin)
                ?? throw new ArgumentException($"User with login {userLogin} does not exists.");

            var commentReports = await _unitOfWork.CommentReports.GetReportsByUserLoginAsync(userLogin);
            return _mapper.Map<IEnumerable<CommentReportResponseDto>>(commentReports);
        }

        public async Task<CommentReportResponseDto> GetCommentReportByIdAsync(int id)
        {
            var commentReport = await _unitOfWork.CommentReports.GetByIdAsync(id)
                ?? throw new ArgumentException($"Comment report with id {id} does not exists.");

            return _mapper.Map<CommentReportResponseDto>(commentReport);
        }

    }
}
