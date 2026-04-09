namespace JewelShrinos.Application.DTOs.Response.Auth;
        public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserResponse? User { get; set; }
    }
 