using JewelShrinos.Core.Entities;
using System.Linq.Expressions;  

namespace JewelShrinos.Core.Interfaces
{

        public class GoogleTokenValidationResult
    {
        public bool IsValid { get; set; }
        public string? GoogleId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
    }
 
    public class EmitInvoiceResponse
    {
        public bool Success { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? TicketNumber { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? ErrorMessage { get; set; }
    }
 
    public class EmitBolletaResponse
    {
        public bool Success { get; set; }
        public string? BolletaNumber { get; set; }
        public string? BolletaUrl { get; set; }
        public string? ErrorMessage { get; set; }
    }
 
    public class GetTicketResponse
    {
        public string Status { get; set; } = null!;
        public string? InvoiceNumber { get; set; }
        public string? ErrorMessage { get; set; }
    }
 
    public class SaleForInvoice
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public List<SaleDetail> SaleDetails { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
 
    public class SaleForBoleta
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = null!;
        public List<SaleDetail> SaleDetails { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
    /// <summary>
    /// Proveedor de tokens JWT
    /// </summary>
        public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserResponse? User { get; set; }
    }
 
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
 
    public class InvoiceResponse
    {
        public bool Success { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? TicketNumber { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? Message { get; set; }
    }
 
    public class InvoiceStatusResponse
    {
        public string Status { get; set; } = null!; // ACEPTADA, RECHAZADA, PROCESANDO
        public string? InvoiceNumber { get; set; }
        public string? ErrorMessage { get; set; }
    }
 
    public class SunatIntegrationResponse
    {
        public string RucNumber { get; set; } = null!;
        public string? ApiProvider { get; set; }
        public bool Status { get; set; }
        public DateTime? LastSync { get; set; }
    }
 
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
    public interface IJwtTokenProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
     //   ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
 
    /// <summary>
    /// Servicio de autenticación con Google
    /// </summary>
    public interface IGoogleOAuthProvider
    {
        Task<GoogleTokenValidationResult> ValidateTokenAsync(string idToken);
    }
 
    /// <summary>
    /// Hash seguro de contraseñas
    /// </summary>
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
 
    /// <summary>
    /// Cliente API de SUNAT
    /// </summary>
    public interface ISunatApiClient
    {
        Task<EmitInvoiceResponse> EmitInvoiceAsync(SaleForInvoice saleData);
        Task<EmitBolletaResponse> EmitBolletaAsync(SaleForBoleta saleData);
        Task<GetTicketResponse> GetTicketAsync(string ticketNumber);
        Task<bool> IsConfiguredAsync();
    }
   public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
 
    public class GoogleLoginRequest
    {
        public string IdToken { get; set; } = null!;
    }
 
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
      public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> GoogleLoginAsync(GoogleLoginRequest request);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
    }

}