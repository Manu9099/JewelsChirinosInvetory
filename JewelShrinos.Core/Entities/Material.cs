namespace JewelShrinos.Core.Entities
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; } = null!; // "Gold 24 Karats"
        public string Type { get; set; } = null!; // 'GOLD', 'SILVER', 'PLATINUM'
        public string? Purity { get; set; } // '24K', '18K', '14K', '925', '950'
        public decimal? ReferencePrice { get; set; } // Precio base por gramo
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}