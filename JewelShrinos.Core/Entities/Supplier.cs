namespace JewelShrinos.Core.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string? RucDni { get; set; } // RUC o DNI en una columna
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}