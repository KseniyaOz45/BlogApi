using AutoMapper;
using BlogApi.DTOs.CommentReport;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class CommentReportMappingProfile : Profile
    {
        public CommentReportMappingProfile() {
            CreateMap<CommentReport, CommentReportResponse>()
                .ForMember(dest => dest.ReasonName, opt => opt.MapFrom(cr => cr.Reason.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(cr => cr.User.Login));

            CreateMap<CommentReportCreateDto, CommentReport>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<CommentReportUpdateDto, CommentReport>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Reason, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
