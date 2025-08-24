using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Category;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Slugify;

namespace BlogApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISlugHelper _slugHelper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ISlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slugHelper = slugHelper;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var existsCategory = await _unitOfWork.Categories.GetCategoryByNameAsync(categoryCreateDto.Name);
            if (existsCategory != null)
                throw new ArgumentException($"Category with name {categoryCreateDto.Name} already exists.");

            var newCategory = new Category()
            {
                Name = categoryCreateDto.Name,
                Slug = _slugHelper.GenerateSlug(categoryCreateDto.Name),
            };

            await _unitOfWork.Categories.AddAsync(newCategory);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CategoryResponseDto>(newCategory);
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var existsCategory = await _unitOfWork.Categories.GetByIdAsync(id)
                ?? throw new ArgumentException($"Category with id {id} does not exists.");

            _unitOfWork.Categories.DeleteAsync(existsCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryByNameAsync(string categoryName)
        {
            var existsCategory = await _unitOfWork.Categories.GetCategoryByNameAsync(categoryName)
                ?? throw new ArgumentException($"Category with name {categoryName} does not exists.");

            _unitOfWork.Categories.DeleteAsync(existsCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryBySlugAsync(string categorySlug)
        {
            var existsCategory = await _unitOfWork.Categories.GetCategoryBySlugAsync(categorySlug)
                ?? throw new ArgumentException($"Category with slug {categorySlug} does not exists.");
            
            _unitOfWork.Categories.DeleteAsync(existsCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id)
                ?? throw new ArgumentException($"Category with id {id} does not exists.");
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto> GetCategoryBySlugAsync(string categorySlug)
        {
            var category = await _unitOfWork.Categories.GetCategoryBySlugAsync(categorySlug)
                ?? throw new ArgumentException($"Category with slug {categorySlug} does not exists.");
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto> GetCategoryByNameAsync(string categoryName) {
            var category = await _unitOfWork.Categories.GetCategoryByNameAsync(categoryName)
                ?? throw new ArgumentException($"Category with name {categoryName} does not exists.");
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto?> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var existsCategory = await _unitOfWork.Categories.GetByIdAsync(id)
                ?? throw new ArgumentException($"Category with id {id} not found. Unable update category.");

            existsCategory.Name = categoryUpdateDto.Name ?? existsCategory.Name;
            existsCategory.Slug = _slugHelper.GenerateSlug(existsCategory.Name);
            existsCategory.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Categories.UpdateAsync(existsCategory);
            await _unitOfWork.SaveAsync();

            var response = _mapper.Map<CategoryResponseDto>(existsCategory);
            return response;
        }
    }
}
