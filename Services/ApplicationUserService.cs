using AutoMapper;
using BlogApi.DTOs.ApplicationUser;
using BlogApi.DTOs.User;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Slugify;

namespace BlogApi.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ISlugHelper _slugHelper;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, ISlugHelper slugHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _slugHelper = slugHelper;
        }

        public async Task<IdentityResult> ChangePassword(int userId, UserPasswordChangeDto userPasswordChangeDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.OldPassword, userPasswordChangeDto.NewPassword);
        }

        public async Task<IdentityResult> DeleteUser(int userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return await _userManager.DeleteAsync(existingUser);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetUserByEmail(string userEmail)
        {
            var existingUser = await _userManager.FindByEmailAsync(userEmail)
                ?? throw new ArgumentException($"User with email {userEmail} does not exists.");

            return _mapper.Map<UserResponseDto?>(existingUser);
        }

        public async Task<UserResponseDto?> GetUserById(int userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new ArgumentException($"User with id {userId} does not exists.");

            return _mapper.Map<UserResponseDto?>(existingUser);
        }

        public async Task<UserResponseDto?> GetUserByLogin(string userLogin)
        {
            var existingUser = await _userManager.FindByNameAsync(userLogin)
                ?? throw new ArgumentException($"User with name|login {userLogin} does not exists.");

            return _mapper.Map<UserResponseDto>(existingUser);

        }

        public async Task<UserResponseDto?> GetUserBySlug(string userSlug)
        {
            var existingUser = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Slug == userSlug)
                ?? throw new ArgumentException($"User with slug {userSlug} does not exist.");

            return _mapper.Map<UserResponseDto?>(existingUser);

        }

        public async Task<SignInResult> Login(UserLoginDto userLoginDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (existingUser == null) {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(
                existingUser.UserName,
                userLoginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterUser(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<ApplicationUser>(userCreateDto);
            user.Slug = _slugHelper.GenerateSlug(userCreateDto.UserName);
            return await _userManager.CreateAsync(user, userCreateDto.Password);
        }

        public async Task<IdentityResult> UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            _mapper.Map(userUpdateDto, user);

            if (!string.IsNullOrWhiteSpace(userUpdateDto.UserName))
            {
                user.Slug = _slugHelper.GenerateSlug(userUpdateDto.UserName);
            }

            return await _userManager.UpdateAsync(user);
        }
    }
}
