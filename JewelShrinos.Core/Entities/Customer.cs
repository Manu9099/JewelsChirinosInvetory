namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// Clientes con soporte para:
    /// - Login local con password
    /// - Login con Google OAuth
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!; // UNIQUE
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? RucDni { get; set; } // Para facturas SUNAT
        
        // Autenticación local
        public string? PasswordHash { get; set; }
        
        // Google OAuth
        public string? GoogleId { get; set; } // UNIQUE
        public string? GoogleProfilePicture { get; set; }
        
        public bool Status { get; set; } = true;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
    }
}