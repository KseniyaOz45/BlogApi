using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Tag;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Slugify;

namespace BlogApi.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlugHelper _slugHelper;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, ISlugHelper slugHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _slugHelper = slugHelper;
            _mapper = mapper;
        }

        public async Task<TagResponseDto> CreateTag(TagCreateDto tagCreateDto)
        {
            var existingTag = await _unitOfWork.Tags.GetTagByNameAsync(tagCreateDto.Name);
            if (existingTag != null) throw new ArgumentException($"Tag with name {tagCreateDto.Name} already exists.");

            var newTag = new Tag
            {
                Name = tagCreateDto.Name,
                Slug = _slugHelper.GenerateSlug(tagCreateDto.Name)
            };

            await _unitOfWork.Tags.AddAsync(newTag);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TagResponseDto>(newTag);
        }

        public async Task<bool> DeleteTag(int tagId)
        {
            var existingTag = await _unitOfWork.Tags.GetByIdAsync(tagId)
                ?? throw new ArgumentException($"Tag with id {tagId} does not exists.");

            _unitOfWork.Tags.DeleteAsync(existingTag);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<TagResponseDto>> GetAllTags()
        {
            return _mapper.Map<IEnumerable<TagResponseDto>>(await _unitOfWork.Tags.GetAllAsync());
        }

        public async Task<TagResponseDto> GetTagById(int tagId)
        {
            var existingTag = await _unitOfWork.Tags.GetByIdAsync(tagId)
                ?? throw new ArgumentException($"Tag with id {tagId} does not exists.");

            return _mapper.Map<TagResponseDto>(existingTag);

        }

        public async Task<TagResponseDto> GetTagBySlug(string tagSlug)
        {
            var existingTag = await _unitOfWork.Tags.GetTagBySlugAsync(tagSlug)
                ?? throw new ArgumentException($"Tag with slug {tagSlug} does not exists.");

            return _mapper.Map<TagResponseDto>(existingTag);
        }

        public async Task<TagResponseDto?> UpdateTag(int tagId, TagUpdateDto tagUpdateDto)
        {
            var existingTag = await _unitOfWork.Tags.GetByIdAsync(tagId)
                ?? throw new ArgumentException($"Tag with id {tagId} does not exists.");

            var tagWithEquelsName = await _unitOfWork.Tags.GetTagByNameAsync(tagUpdateDto.Name);
            if (tagWithEquelsName != null && tagWithEquelsName.Id != tagId)
                throw new ArgumentException($"Tag with name {tagUpdateDto.Name} already exists.");

            existingTag.Name = tagUpdateDto.Name ?? existingTag.Name;
            existingTag.Slug = _slugHelper.GenerateSlug(tagUpdateDto.Name ?? existingTag.Name);

            _unitOfWork.Tags.UpdateAsync(existingTag);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TagResponseDto>(existingTag);
        }
    }
}
