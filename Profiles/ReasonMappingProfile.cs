using AutoMapper;
using BlogApi.DTOs.Reason;
using BlogApi.Models;

namespace BlogApi.Profiles
{
    public class ReasonMappingProfile : Profile
    {
        public ReasonMappingProfile() {
            CreateMap<Reason, ReasonResponseDto>()
                .ForMember(dest => dest.PostReportIds, opt => opt.MapFrom(r => r.PostReports.Select(pr => pr.Id).ToList()))
                .ForMember(dest => dest.CommentReportIds, opt => opt.MapFrom(r => r.CommentReports.Select(cr => cr.Id).ToList()));

            CreateMap<ReasonCreateDto, Reason>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<ReasonUpdateDto, Reason>()
                .ForMember(dest => dest.PostReports, opt => opt.Ignore())
                .ForMember(dest => dest.CommentReports, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
