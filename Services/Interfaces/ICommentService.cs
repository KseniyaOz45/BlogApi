using BlogApi.DTOs.Comment;

namespace BlogApi.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDto>> GetAllComments();
        Task<IEnumerable<CommentResponseDto>> GetAllCommentsToPost(int postId);
        Task<IEnumerable<CommentResponseDto>> GetAllCommentsByUser(int userId);
        Task<CommentResponseDto> CreateComment(int userId, CommentCreateDto commentCreateDto);
        Task<bool> DeleteComment(int commentId);
    }
}
