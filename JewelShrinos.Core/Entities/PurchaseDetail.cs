namespace JewelShrinos.Core.Entities
{
 public class PurchaseDetail
{
    public int PurchaseDetailId { get; set; }

    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Subtotal { get; set; }
    public int QuantityReceived { get; set; }
    public string? Observations { get; set; }
    public DateTime CreatedAt { get; set; }
    
}
}