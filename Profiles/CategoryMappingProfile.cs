using AutoMapper;
using BlogApi.DTOs.Category;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest => dest.PostTitles, opt => opt.MapFrom(src => src.Posts != null ? src.Posts.Select(b => b.Title).ToList() : new List<string>()));

            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Slug, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Slug, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
