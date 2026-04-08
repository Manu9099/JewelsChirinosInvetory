// ReturnDetail.cs
namespace JewelShrinos.Core.Entities
{
    public class ReturnDetail
    {
        public int ReturnDetailId { get; set; }
        public int ReturnId { get; set; }
        public int SaleDetailId { get; set; }
        
        public int QuantityReturned { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Return? Return { get; set; }
        public virtual SaleDetail? SaleDetail { get; set; }
    }
}