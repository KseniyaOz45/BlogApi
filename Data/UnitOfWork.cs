using BlogApi.Repositories;
using BlogApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlogApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;

        private IPostRepository _posts;
        private ICategoryRepository _categories;
        private ITagRepository _tags;
        private ICommentRepository _comments;
        private ILikeRepository _likes;
        private IPostReportRepository _postReports;
        private IReasonRepository _reasons;
        private ICommentReportRepository _commentReports;

        public IPostRepository Posts => _posts ??= new PostRepository(_context);
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
        public ITagRepository Tags => _tags ??= new TagRepository(_context);
        public ICommentRepository Comments => _comments ??= new CommentRepository(_context);
        public ILikeRepository Likes => _likes ??= new LikeRepository(_context);
        public IPostReportRepository PostReports => _postReports ??= new PostReportRepository(_context);
        public IReasonRepository Reasons => _reasons ??= new ReasonRepository(_context);
        public ICommentReportRepository CommentReports => _commentReports ??= new CommentReportRepository(_context);

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
