using BlogApi.DTOs.Tag;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags() {
            var tags = await _tagService.GetAllTags();
            return Ok(tags);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTagById(int id) {
            var tag = await _tagService.GetTagById(id);
            return Ok(tag);
        }

        [HttpGet("by-slug/{tagSlug}")]
        public async Task<IActionResult> GetTagBySlug(string tagSlug) {
            var tag = await _tagService.GetTagBySlug(tagSlug);
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDto tagCreateDto) {
            var result = await _tagService.CreateTag(tagCreateDto);
            return CreatedAtAction(nameof(GetTagById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] TagUpdateDto tagUpdateDto) {
            var result = await _tagService.UpdateTag(id, tagUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag(int id) {
            var result = await _tagService.DeleteTag(id);
            return NoContent();
        }
    }
}
