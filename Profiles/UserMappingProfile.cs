using AutoMapper;
using BlogApi.DTOs.User;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<ApplicationUser, UserResponseDto>()
                .ForMember(dest => dest.CommentIds, opt => opt.MapFrom(u => u.Comments != null ? u.Comments.Select(c => c.Id).ToList() : new List<int>()))
                .ForMember(dest => dest.CommentReportIds, opt => opt.MapFrom(u => u.CommentReports != null ? u.CommentReports.Select(cr => cr.Id).ToList() : new List<int>()))
                .ForMember(dest => dest.LikeIds, opt => opt.MapFrom(u => u.Likes != null ? u.Likes.Select(l => l.Id).ToList() : new List<int>()))
                .ForMember(dest => dest.PostReportIds, opt => opt.MapFrom(u => u.PostReports != null ? u.PostReports.Select(pr => pr.Id).ToList() : new List<int>()))
                .ForMember(dest => dest.PostTitles, opt => opt.MapFrom(u => u.Posts != null ? u.Posts.Select(p => p.Title).ToList() : new List<string>()))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(u => u.AvatarUrl ?? string.Empty));

            CreateMap<UserResponseDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UserUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
