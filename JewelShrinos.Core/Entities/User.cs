namespace JewelShrinos.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        
        public string Role { get; set; } = "SELLER"; // ADMIN, MANAGER, SELLER, WAREHOUSE
          public string? ProfilePictureUrl { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }

        public DateTime? LastAccess { get; set; }

        public string AuthProvider { get; set; } = "LOCAL";  // LOCAL, GOOGLE, FACEBOOK, etc.
        public bool Status { get; set; } = true;
        public string? GoogleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
    }
}
 
