using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Like;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<LikeResponseDto> CreateLike(int userId, LikeCreateDto likeCreateDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var post = await _unitOfWork.Posts.GetByIdAsync(likeCreateDto.PostId)
                ?? throw new ArgumentException($"Post with id {likeCreateDto.PostId} does not exists.");

            var newLike = new Like
            {
                PostId = likeCreateDto.PostId,
                UserId = user.Id
            };

            await _unitOfWork.Likes.AddAsync(newLike);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<LikeResponseDto>(newLike);
        }

        public async Task<bool> DeleteLike(int likeId)
        {
            var like = await _unitOfWork.Likes.GetByIdAsync(likeId)
                ?? throw new ArgumentException($"Like witj id {likeId} does not exists.");

            _unitOfWork.Likes.DeleteAsync(like);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<LikeResponseDto>> GetAllLikes()
        {
            return _mapper.Map<IEnumerable<LikeResponseDto>>(await _unitOfWork.Likes.GetAllAsync());
        }

        public async Task<LikeResponseDto> GetLikeById(int likeId)
        {
            return _mapper.Map<LikeResponseDto>(await _unitOfWork.Likes.GetByIdAsync(likeId));
        }

        public async Task<IEnumerable<LikeResponseDto>> GetLikesByPostSlug(string postSlug)
        {
            var post = await _unitOfWork.Posts.GetPostBySlugAsync(postSlug)
                ?? throw new ArgumentException($"Post with slug {postSlug} does not exists.");

            return _mapper.Map<IEnumerable<LikeResponseDto>>(await _unitOfWork.Likes.GetLikesByPostAsync(postSlug));
        }
    }
}
