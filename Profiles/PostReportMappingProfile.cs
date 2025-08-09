using AutoMapper;
using BlogApi.DTOs.PostReport;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class PostReportMappingProfile : Profile
    {
        public PostReportMappingProfile() {
            CreateMap<PostReport, PostReportResponseDto>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(pr => pr.Post.Title))
                .ForMember(dest => dest.ReasonName, opt => opt.MapFrom(pr => pr.Reason.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(pr => pr.User.Login));

            CreateMap<PostReportCreateDto, PostReport>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<PostReportUpdateDto, PostReport>()
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
