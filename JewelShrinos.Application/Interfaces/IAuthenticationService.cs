using JewelShrinos.Application.DTOs.Request.Auth;
using JewelShrinos.Application.DTOs.Response.Auth;

namespace JewelShrinos.Application.Interfaces;

public interface IAuthenticationService
{
    Task<UserResponse> RegisterAsync(RegisterUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> GoogleLoginAsync(GoogleLoginRequest request);
    Task<UserResponse?> GetByIdAsync(int userId);
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
    Task<bool> DisableAsync(int userId);
}