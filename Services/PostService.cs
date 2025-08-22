using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Post;
using BlogApi.DTOs.Tag;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Slugify;

namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SlugHelper _slugHelper;
        private readonly IWebHostEnvironment _env;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _slugHelper = slugHelper;
        }

        public async Task<PostResponseDto> CreatePost(int userId, PostCreateDto postCreateDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var category = await _unitOfWork.Categories.GetByIdAsync(postCreateDto.CategoryId)
                ?? throw new ArgumentException($"Category with id {postCreateDto.CategoryId} does not exists.");

            var imagePath = await SaveImageAsync(postCreateDto.Image);

            var newPost = new Post
            {
                Title = postCreateDto.Title,
                Slug = _slugHelper.GenerateSlug(postCreateDto.Title),
                Content = postCreateDto.Content,
                ImageUrl = imagePath,
                ViewCount = 0,
                IsPublished = postCreateDto.IsPublished,
                UserId = userId,
                CategoryId = postCreateDto.CategoryId
            };

            await _unitOfWork.Posts.AddAsync(newPost);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PostResponseDto>(newPost);
        }

        public async Task<bool> ViewPost(string postSlug)
        {
            var post = await _unitOfWork.Posts.GetPostBySlugAsync(postSlug)
                ?? throw new ArgumentException($"Post with slug {postSlug} does not exists.");

            post.ViewCount++;
            _unitOfWork.Posts.UpdateAsync(post);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId)
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");

            _unitOfWork.Posts.DeleteAsync(post);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPosts()
        {
            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetAllAsync());
        }

        public async Task<IEnumerable<PostResponseDto>> GetLatestPosts()
        {
            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetRecentPostsAsync(3));
        }

        public async Task<IEnumerable<PostResponseDto>> GetMostPopularPosts()
        {
            var popularPosts = (await _unitOfWork.Posts.GetAllAsync()).OrderByDescending(p => p.ViewCount).ThenBy(p => p.Likes.Count).Take(3).ToList();
            return _mapper.Map<IEnumerable<PostResponseDto>>(popularPosts);
        }

        public async Task<PostResponseDto> GetPostById(int postId)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId)
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");
            return _mapper.Map<PostResponseDto>(post);
        }

        public async Task<PostResponseDto> GetPostBySlug(string postSlug)
        {
            var post = await _unitOfWork.Posts.GetPostBySlugAsync(postSlug)
                ?? throw new ArgumentException($"Post with slug {postSlug} does not exists.");
            return _mapper.Map<PostResponseDto>(post);
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByCategoryId(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId)
                ?? throw new ArgumentException($"Category with id {categoryId} does not exists.");

            var posts = await _unitOfWork.Posts.GetPostsByCategoryAsync(category.Name);
            return _mapper.Map<IEnumerable<PostResponseDto>>(posts);
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByCategoryName(string categoryName)
        {
            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByCategoryAsync(categoryName));

        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByTags(List<int> tagsIds)
        {
            if (tagsIds == null || tagsIds.Count == 0) {
                throw new ArgumentException($"Tags list must contain primary 1 element and not be null.");
            }

            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByTagsAsync(tagsIds));
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByTitle(string postTitle)
        {
            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.SearchPostsByValueAsync(postTitle));
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByUserAsync(user.UserName));
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                ?? throw new ArgumentException($"User with name {userName} does not exists.");

            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByUserAsync(user.UserName));
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsDraftsByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByUserAndTypeAsync(user.UserName, false));
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsDraftsByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                ?? throw new ArgumentException($"User with name {userName} does not exists.");

            return _mapper.Map<IEnumerable<PostResponseDto>>(await _unitOfWork.Posts.GetPostsByUserAndTypeAsync(user.UserName, false));
        }

        public async Task<PostResponseDto?> UpdatePost(int userId, int postId, PostUpdateDto postUpdateDto)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId)
                ?? throw new ArgumentException($"Post with id {postId} does not exists.");

            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            var category = await _unitOfWork.Categories.GetByIdAsync(postUpdateDto.CategoryId.Value)
                ?? throw new ArgumentException($"Category with id {postUpdateDto.CategoryId.Value} does not exists.");

            post.UpdatedAt = DateTime.Now;
            post.Title = postUpdateDto.Title ?? post.Title;
            post.Slug = _slugHelper.GenerateSlug(post.Title);
            post.Content = postUpdateDto.Content ?? post.Content;
            post.UserId = user.Id;
            post.CategoryId = category.Id;

            if (postUpdateDto.Image != null) {
                DeleteImageFile(post.ImageUrl);
                post.ImageUrl = await SaveImageAsync(postUpdateDto.Image);
            }

            _unitOfWork.Posts.UpdateAsync(post);
            await _unitOfWork.SaveAsync();

            var responsePost = _mapper.Map<PostResponseDto>(post);
            responsePost.CategoryName = category.Name;
            responsePost.UserName = user.UserName;

            return responsePost;
        }

        //-------------------------------------------------------------------------------

        private async Task<string?> SaveImageAsync(IFormFile? cover)
        {
            if (cover == null) return null;

            var folderPath = Path.Combine(_env.WebRootPath, "images", "posts_images");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cover.CopyToAsync(stream);
            }

            // Возвращаем относительный путь (для API)
            return $"/images/posts_images/{fileName}";
        }

        private void DeleteImageFile(string? coverUrl)
        {
            if (string.IsNullOrEmpty(coverUrl)) return;

            var path = Path.Combine(_env.WebRootPath, coverUrl.TrimStart('/'));
            if (File.Exists(path))
                File.Delete(path);
        }

       
    }
}
