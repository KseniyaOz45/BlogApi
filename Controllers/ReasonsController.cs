using BlogApi.DTOs.Reason;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonService _reasonService;

        public ReasonsController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReasons() {
            var reasons = await _reasonService.GetAllReasons();
            return Ok(reasons);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReasonById(int id) {
            var reason = await _reasonService.GetReasonById(id);
            return Ok(reason);
        }

        [HttpGet("by-slug/{reasonSlug}")]
        public async Task<IActionResult> GetReasonBySlug(string reasonSlug) {
            var reason = await _reasonService.GetReasonBySlug(reasonSlug);
            return Ok(reason);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReason([FromBody] ReasonCreateDto reasonCreateDto) {
            var result = await _reasonService.CreateReason(reasonCreateDto);
            return CreatedAtAction(nameof(GetReasonById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateReason(int id, [FromBody] ReasonUpdateDto reasonUpdateDto) {
            var result = await _reasonService.UpdateReason(id, reasonUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReason(int id) {
            var result = await _reasonService.DeleteReason(id);
            return NoContent();
        }

    }
}
