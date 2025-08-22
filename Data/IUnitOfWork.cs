using BlogApi.Repositories.Interfaces;

namespace BlogApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ICommentReportRepository CommentReports { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
        IPostReportRepository PostReports { get; }
        IPostRepository Posts { get; }
        IReasonRepository Reasons { get; }
        ITagRepository Tags { get; }

        Task<int> SaveAsync();
    }
}
