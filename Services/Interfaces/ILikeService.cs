using BlogApi.DTOs.Like;

namespace BlogApi.Services.Interfaces
{
    public interface ILikeService
    {
        Task<IEnumerable<LikeResponseDto>> GetAllLikes();
        Task<IEnumerable<LikeResponseDto>> GetLikesByPostSlug(string postSlug);
        Task<LikeResponseDto> GetLikeById(int likeId);
        Task<LikeResponseDto> CreateLike(int userId, LikeCreateDto likeCreateDto);
        Task<bool> DeleteLike(int likeId);
    }
}
