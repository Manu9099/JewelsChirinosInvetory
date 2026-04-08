namespace JewelShrinos.Core.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; } = null!; // C-YYYYMMDD-NNN
        public int SupplierId { get; set; }
        
        public DateTime PurchaseDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? TotalAmount { get; set; }
 
        public string PurchaseStatus { get; set; } = "PENDING"; 
        // 'PENDING', 'CONFIRMED', 'RECEIVED', 'PARTIAL', 'CANCELLED'
        
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}