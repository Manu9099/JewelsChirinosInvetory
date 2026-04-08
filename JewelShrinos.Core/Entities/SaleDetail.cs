namespace JewelShrinos.Core.Entities
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal LineDiscount { get; set; } = 0;
        
        public string DetailStatus { get; set; } = "SOLD";
        // SOLD, RETURNED, CANCELLED
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Sale? Sale { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; } = new List<ReturnDetail>();
    }
}