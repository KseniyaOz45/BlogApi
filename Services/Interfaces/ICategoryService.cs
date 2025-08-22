using BlogApi.DTOs.Category;

namespace BlogApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryBySlugAsync(string categorySlug);
        Task<CategoryResponseDto> GetCategoryByNameAsync(string categoryName);
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryResponseDto?> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);
        Task<bool> DeleteCategoryByIdAsync(int id);
        Task<bool> DeleteCategoryBySlugAsync(string categorySlug);
        Task<bool> DeleteCategoryByNameAsync(string categoryName);
    }
}
