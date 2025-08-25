using BlogApi.DTOs.ApplicationUser;
using BlogApi.DTOs.User;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public UsersController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id) {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("by-slug/{userSlug}")]
        public async Task<IActionResult> GetUserBySlug(string userSlug) {
            var user = await _userService.GetUserBySlug(userSlug);
            return Ok(user);
        }

        [HttpGet("by-email/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail(string userEmail) {
            var user = await _userService.GetUserByEmail(userEmail);
            return Ok(user);
        }

        [HttpGet("by-name/{userName}")]
        public async Task<IActionResult> GetUserByUsername(string userName) {
            var user = await _userService.GetUserByLogin(userName);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserCreateDto userCreateDto) {
            var result = await _userService.RegisterUser(userCreateDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDto) {
            var result = await _userService.Login(userLoginDto);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() {
            await _userService.Logout();
            return Ok();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangeDto userPasswordChangeDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _userService.ChangePassword(int.Parse(userId), userPasswordChangeDto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _userService.UpdateUser(int.Parse(userId), userUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id) {
            var result = await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
