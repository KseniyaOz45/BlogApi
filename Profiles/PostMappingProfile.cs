using AutoMapper;
using BlogApi.DTOs.Post;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile() {
            CreateMap<Post, PostResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(p => p.User.Login))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(p => p.Category.Name))
                .ForMember(dest => dest.CommentIds, opt => opt.MapFrom(p => p.Comments.Select(c => c.Id).ToList()))
                .ForMember(dest => dest.PostReportsIds, opt => opt.MapFrom(p => p.PostReports.Select(pr => pr.Id).ToList()))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(p => p.Tags.Select(t => t.Name).ToList()))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(p => p.Likes.Count));

            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<PostUpdateDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
