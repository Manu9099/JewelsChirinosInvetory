using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JewelShrinos.Application.DTOs.Request.Auth;
using JewelShrinos.Application.DTOs.Response.Auth;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JewelShrinos.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IRepository<User> userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<UserResponse> RegisterAsync(RegisterUserRequest request)
    {
        ValidateRegisterRequest(request);

        var email = request.Email.Trim().ToLowerInvariant();
        var username = request.Username.Trim().ToLowerInvariant();
        var role = NormalizeRole(request.Role);

        var emailExists = await _userRepository.AnyAsync(u => u.Email.ToLower() == email);
        if (emailExists)
            throw new InvalidOperationException("Ya existe un usuario con ese email.");

        var usernameExists = await _userRepository.AnyAsync(u => u.Username.ToLower() == username);
        if (usernameExists)
            throw new InvalidOperationException("Ya existe un usuario con ese username.");

        var user = new User
        {
            Username = username,
            Email = email,
            FullName = request.FullName.Trim(),
            PasswordHash = HashPassword(request.Password),
            Role = role,
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return MapUser(user);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new InvalidOperationException("El email es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new InvalidOperationException("La contraseña es obligatoria.");

        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _userRepository.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
        if (user is null)
            throw new InvalidOperationException("Credenciales inválidas.");

        if (!user.Status)
            throw new InvalidOperationException("El usuario está desactivado.");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Credenciales inválidas.");

        user.LastAccess = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.SaveChangesAsync();

        return new LoginResponse
        {
            Success = true,
            Message = "Login correcto.",
            AccessToken = GenerateJwt(user),
            RefreshToken = null,
            User = MapUser(user)
        };
    }

    public async Task<UserResponse?> GetByIdAsync(int userId)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.UserId == userId);
        return user is null ? null : MapUser(user);
    }

    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null) return false;

        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            throw new InvalidOperationException("La contraseña actual es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            throw new InvalidOperationException("La nueva contraseña es obligatoria.");

        if (!VerifyPassword(request.CurrentPassword, user.PasswordHash))
            throw new InvalidOperationException("La contraseña actual no es correcta.");

        if (request.NewPassword.Length < 6)
            throw new InvalidOperationException("La nueva contraseña debe tener al menos 6 caracteres.");

        user.PasswordHash = HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DisableAsync(int userId)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null) return false;

        user.Status = false;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.SaveChangesAsync();
        return true;
    }

    private string GenerateJwt(User user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key no configurado.");
        var issuer = jwtSection["Issuer"] ?? "JewelShrinos";
        var audience = jwtSection["Audience"] ?? "JewelShrinosUsers";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
            new("username", user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserResponse MapUser(User user)
    {
        return new UserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role
        };
    }

    private static void ValidateRegisterRequest(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
            throw new InvalidOperationException("El username es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new InvalidOperationException("El email es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.FullName))
            throw new InvalidOperationException("El nombre completo es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new InvalidOperationException("La contraseña es obligatoria.");

        if (request.Password.Length < 6)
            throw new InvalidOperationException("La contraseña debe tener al menos 6 caracteres.");
    }

    private static string NormalizeRole(string? role)
    {
        var normalized = string.IsNullOrWhiteSpace(role)
            ? "SELLER"
            : role.Trim().ToUpperInvariant();

        return normalized switch
        {
            "ADMIN" => "ADMIN",
            "MANAGER" => "MANAGER",
            "SELLER" => "SELLER",
            "WAREHOUSE" => "WAREHOUSE",
            _ => throw new InvalidOperationException("Role no válido.")
        };
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expectedHash = Convert.FromBase64String(parts[1]);

        byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);

        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}