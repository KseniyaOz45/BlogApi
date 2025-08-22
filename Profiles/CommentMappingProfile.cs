using AutoMapper;
using BlogApi.DTOs.Comment;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile() {
            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(c => c.Post != null ? c.Post.Title : "Undefined"))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(c => c.User != null ? c.User.UserName : "Undefined"))
                .ForMember(dest => dest.CommentReportsCount, opt => opt.MapFrom(c => c.CommentReports != null ? c.CommentReports.Count : 0))
                .ForMember(dest => dest.CommentReportsIds, opt => opt.MapFrom(c => c.CommentReports != null ? c.CommentReports.Select(cr => cr.Id).ToList() : new List<int>()));

            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore());

            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
