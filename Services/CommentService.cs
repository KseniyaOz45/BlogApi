using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Comment;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CommentResponseDto> CreateComment(int userId, CommentCreateDto commentCreateDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var post = await _unitOfWork.Posts.GetByIdAsync(commentCreateDto.PostId)
                ?? throw new ArgumentException($"Post with id {commentCreateDto.PostId} does not exists.");

            var newComment = new Comment()
            {
                Content = commentCreateDto.Content,
                UserId = user.Id,
                PostId = commentCreateDto.PostId
            };

            await _unitOfWork.Comments.AddAsync(newComment);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CommentResponseDto>(newComment);
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId)
                ?? throw new ArgumentException($"Comment with id {commentId} does not exists.");

            _unitOfWork.Comments.DeleteAsync(comment);
            _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<CommentResponseDto>> GetAllComments()
        {
            return _mapper.Map<IEnumerable<CommentResponseDto>>(await _unitOfWork.Comments.GetAllAsync());
        }

        public async Task<IEnumerable<CommentResponseDto>> GetAllCommentsByUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<IEnumerable<CommentResponseDto>>(await _unitOfWork.Comments.GetCommentsByUserLoginAsync(user.UserName));
        }

        public async Task<IEnumerable<CommentResponseDto>> GetAllCommentsToPost(int postId)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId)
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");

            return _mapper.Map<IEnumerable<CommentResponseDto>>(await _unitOfWork.Comments.GetCommentsByPostSlugAsync(post.Slug));
        }
    }
}
