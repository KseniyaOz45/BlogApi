using AutoMapper;
using BlogApi.DTOs.Like;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class LikeMappingProfile : Profile
    {
        public LikeMappingProfile() {
            CreateMap<Like, LikeResponseDto>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(l => l.Post != null ? l.Post.Title : "Undefined"))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(l => l.User != null ? l.User.UserName : "Undefined"));

            CreateMap<LikeCreateDto, Like>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
