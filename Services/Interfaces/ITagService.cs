using BlogApi.DTOs.Tag;

namespace BlogApi.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponseDto>> GetAllTags();
        Task<TagResponseDto> GetTagById(int tagId);
        Task<TagResponseDto> GetTagBySlug(string tagSlug);
        Task<TagResponseDto> CreateTag(TagCreateDto tagCreateDto);
        Task<TagResponseDto?> UpdateTag(int tagId, TagUpdateDto tagUpdateDto);
        Task<bool> DeleteTag(int tagId);
    }
}
