using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.PostReport;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Services
{
    public class PostReportService : IPostReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostReportService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PostReportResponseDto> CreatePostReport(int userId, PostReportCreateDto postReportCreateDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) 
                ?? throw new ArgumentException($"User with id {userId} does not exists.");
            var post = await _unitOfWork.Posts.GetByIdAsync(postReportCreateDto.PostId) 
                ?? throw new ArgumentException($"Post with id {postReportCreateDto.PostId} does not exists.");
            var reason = await _unitOfWork.Reasons.GetByIdAsync(postReportCreateDto.ReasonId) 
                ?? throw new ArgumentException($"Reason with id {postReportCreateDto.ReasonId} does not exists.");

            var newPostReport = new PostReport()
            {
                Content = postReportCreateDto.Content ?? "No content",
                UserId = userId,
                PostId = post.Id,
                ReasonId = reason.Id
            };

            await _unitOfWork.PostReports.AddAsync(newPostReport);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PostReportResponseDto>(newPostReport);
        }

        public async Task<bool> DeletePostReport(int postReportId)
        {
            var postReport = await _unitOfWork.PostReports.GetByIdAsync(postReportId)
                ?? throw new ArgumentException($"Post report with id {postReportId} does not exists.");

            _unitOfWork.PostReports.DeleteAsync(postReport);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetAllPostReports()
        {
            return _mapper.Map<IEnumerable<PostReportResponseDto>>(await _unitOfWork.PostReports.GetAllAsync());
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByDate(DateTime date)
        {
            return _mapper.Map<IEnumerable<PostReportResponseDto>>((await _unitOfWork.PostReports.GetAllAsync()).Where(pr => pr.CreatedAt >= date));
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByPostId(int postId)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId) 
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");

            return _mapper.Map<IEnumerable<PostReportResponseDto>>(await _unitOfWork.PostReports.GetReportsByPostSlugAsync(post.Slug));
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByReasonId(int reasonId)
        {
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reasonId)
                ?? throw new ArgumentException($"Reason with id {reasonId} does not exists.");

            return _mapper.Map<IEnumerable<PostReportResponseDto>>(await _unitOfWork.PostReports.GetReportsByReasonSlugAsync(reason.Slug));
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserAndDate(int userId, DateTime date)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) 
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<IEnumerable<PostReportResponseDto>>((await _unitOfWork.PostReports.GetAllAsync()).Where(pr => pr.UserId == userId && pr.CreatedAt == date));
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserAndPostAndDate(int userId, int postId, DateTime date)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var post = await _unitOfWork.Posts.GetByIdAsync(postId) 
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");

            return _mapper.Map<IEnumerable<PostReportResponseDto>>((await _unitOfWork.PostReports.GetAllAsync()).Where(pr => pr.UserId == userId && pr.PostId == postId && pr.CreatedAt == date));
        }

        public async Task<IEnumerable<PostReportResponseDto>> GetPostReportsByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<IEnumerable<PostReportResponseDto>>(await _unitOfWork.PostReports.GetReportsByUserLoginAsync(user.UserName));
        }
    }
}
