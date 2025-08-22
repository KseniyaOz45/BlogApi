using AutoMapper;
using BlogApi.Data;
using BlogApi.DTOs.Reason;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Slugify;

namespace BlogApi.Services
{
    public class ReasonService : IReasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISlugHelper _slugHelper;

        public ReasonService(IUnitOfWork unitOfWork, IMapper mapper, ISlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slugHelper = slugHelper;
        }

        public async Task<ReasonResponseDto> CreateReason(ReasonCreateDto reasonCreateDto)
        {
            var newReason = new Reason()
            {
                Name = reasonCreateDto.Name,
                Slug = _slugHelper.GenerateSlug(reasonCreateDto.Name)
            };

            await _unitOfWork.Reasons.AddAsync(newReason);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ReasonResponseDto>(newReason);
        }

        public async Task<bool> DeleteReason(int reasonId)
        {
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reasonId)
                ?? throw new ArgumentException($"Reason with id {reasonId} does not exists.");

            _unitOfWork.Reasons.DeleteAsync(reason);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ReasonResponseDto>> GetAllReasons()
        {
            return _mapper.Map<IEnumerable<ReasonResponseDto>>(await _unitOfWork.Reasons.GetAllAsync());
        }

        public async Task<ReasonResponseDto> GetReasonById(int reasonId)
        {
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reasonId)
                ?? throw new ArgumentException($"reason with id {reasonId} does not exists.");
            return _mapper.Map<ReasonResponseDto>(reason);
        }

        public async Task<ReasonResponseDto> GetReasonBySlug(string reasonSlug)
        {
            var reason = await _unitOfWork.Reasons.GetReasonBySlugAsync(reasonSlug)
                ?? throw new ArgumentException($"reason with slug {reasonSlug} does not exists.");
            return _mapper.Map<ReasonResponseDto>(reason);
        }

        public async Task<ReasonResponseDto?> UpdateReason(int reasonId, ReasonUpdateDto reasonUpdateDto)
        {
            var reason = await _unitOfWork.Reasons.GetByIdAsync(reasonId)
                ?? throw new ArgumentException($"reason with id {reasonId} does not exists.");

            reason.UpdatedAt = DateTime.UtcNow;
            reason.Name = reasonUpdateDto.Name ?? reason.Name;
            reason.Slug = _slugHelper.GenerateSlug(reasonUpdateDto.Name ?? reason.Name);

            _unitOfWork.Reasons.UpdateAsync(reason);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ReasonResponseDto>(reason);

        }
    }
}
