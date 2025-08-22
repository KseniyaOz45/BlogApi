using BlogApi.DTOs.ApplicationUser;
using BlogApi.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsers();
        Task<UserResponseDto?> GetUserById(int userId);
        Task<UserResponseDto?> GetUserBySlug(string userSlug);
        Task<UserResponseDto?> GetUserByEmail(string userEmail);
        Task<UserResponseDto?> GetUserByLogin(string userLogin);

        Task<IdentityResult> RegisterUser(UserCreateDto userCreateDto);
        Task<SignInResult> Login(UserLoginDto userLoginDto);
        Task Logout();

        Task<IdentityResult> ChangePassword(int userId, UserPasswordChangeDto userPasswordChangeDto);
        Task<IdentityResult> UpdateUser(int userId, UserUpdateDto userUpdateDto);
        Task<IdentityResult> DeleteUser(int userId);

    }
}
