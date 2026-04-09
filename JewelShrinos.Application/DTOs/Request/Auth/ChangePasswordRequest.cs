  namespace JewelShrinos.Application.DTOs.Request.Auth;
   public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }