using BlogApi.DTOs.Reason;

namespace BlogApi.Services.Interfaces
{
    public interface IReasonService
    {
        Task<IEnumerable<ReasonResponseDto>> GetAllReasons();
        Task<ReasonResponseDto> GetReasonById(int reasonId);
        Task<ReasonResponseDto> GetReasonBySlug(string reasonSlug);
        Task<ReasonResponseDto> CreateReason(ReasonCreateDto reasonCreateDto);
        Task<ReasonResponseDto?> UpdateReason(int reasonId, ReasonUpdateDto reasonUpdateDto);
        Task<bool> DeleteReason(int reasonId);
    }
}
