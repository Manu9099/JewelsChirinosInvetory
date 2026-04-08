namespace JewelShrinos.Core.Entities
{
    public class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public int QuantityReceived { get; set; } = 0;
        public string? Observations { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Purchase? Purchase { get; set; }
        public virtual Product? Product { get; set; }
    }
}