using BlogApi.DTOs.Category;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories() {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id) {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<IActionResult> GetCategoryBySlug(string slug) {
            var category = await _categoryService.GetCategoryBySlugAsync(slug);
            return Ok(category);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name) {
            var category = await _categoryService.GetCategoryByNameAsync(name);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto) {
            var result = await _categoryService.CreateCategoryAsync(categoryCreateDto);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto) {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(int id) {
            var result = await _categoryService.DeleteCategoryByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("by-name/{name}")]
        public async Task<IActionResult> DeleteCategoryByName(string name) {
            var result = await _categoryService.DeleteCategoryByNameAsync(name);
            return Ok(result);
        }

        [HttpDelete("by-slug/{slug}")]
        public async Task<IActionResult> DeleteCategoryBySlug(string slug) {
            var result = await _categoryService.DeleteCategoryBySlugAsync(slug);
            return Ok(result);
        }

    }
}
