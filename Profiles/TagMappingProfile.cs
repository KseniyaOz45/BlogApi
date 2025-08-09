using AutoMapper;
using BlogApi.DTOs.Tag;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile() {
            CreateMap<Tag, TagResponseDto>()
                .ForMember(dest => dest.PostTitles, opt => opt.MapFrom(t => t.Posts.Select(p => p.Title).ToList()));

            CreateMap<TagCreateDto, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<TagUpdateDto, Tag>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
