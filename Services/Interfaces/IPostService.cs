using BlogApi.DTOs.Like;
using BlogApi.DTOs.Post;
using BlogApi.DTOs.Tag;

namespace BlogApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseDto>> GetAllPosts();
        Task<IEnumerable<PostResponseDto>> GetMostPopularPosts();
        Task<IEnumerable<PostResponseDto>> GetLatestPosts();
        Task<IEnumerable<PostResponseDto>> GetPostsByUserName(string userName);
        Task<IEnumerable<PostResponseDto>> GetPostsByUserId(int userId);
        Task<IEnumerable<PostResponseDto>> GetPostsDraftsByUserName(string userName);
        Task<IEnumerable<PostResponseDto>> GetPostsDraftsByUserId(int userId);
        Task<IEnumerable<PostResponseDto>> GetPostsByCategoryId(int categoryId);
        Task<IEnumerable<PostResponseDto>> GetPostsByCategoryName(string categoryName);
        Task<IEnumerable<PostResponseDto>> GetPostsByTags(List<int> tagIds);
        Task<IEnumerable<PostResponseDto>> GetPostsByTitle(string postTitle);
        Task<PostResponseDto> GetPostById(int postId);
        Task<PostResponseDto> GetPostBySlug(string postSlug);
        Task<bool> ViewPost(string postSlug);
        Task<PostResponseDto> CreatePost(int userId, PostCreateDto postCreateDto);
        Task<PostResponseDto?> UpdatePost(int userId, int postId, PostUpdateDto postUpdateDto);
        Task<bool> DeletePost(int postId);
    }
}
