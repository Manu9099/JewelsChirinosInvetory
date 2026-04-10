     namespace JewelShrinos.Application.DTOs.Request.Auth;
     
   public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

            public class RegisterUserRequest
            {
                public string Username { get; set; } = null!;
                public string Email { get; set; } = null!;
                public string FullName { get; set; } = null!;
                public string Password { get; set; } = null!;
                public string Role { get; set; } = "SELLER";
            }