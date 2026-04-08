namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// Devoluciones - Cuando se APRUEBA:
    /// 1. 🔥 INVENTARIO SE RESTAURA AUTOMÁTICAMENTE
    /// 2. 🔥 Se registra movimiento
    /// </summary>
    public class Return
    {
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!; // RET-YYYYMMDD-NNN
        public int SaleId { get; set; }
        public int? CustomerId { get; set; }
        
        public string Reason { get; set; } = null!;
        public decimal? RefundAmount { get; set; }
        
        public string ReturnStatus { get; set; } = "PENDING";
        // PENDING, APPROVED, REJECTED, PROCESSED
        
        public string? CreatedBy { get; set; }
        public string? Observations { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessingDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Sale? Sale { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; } = new List<ReturnDetail>();
    }
}
 
