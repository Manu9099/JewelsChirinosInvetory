   namespace JewelShrinos.Application.DTOs.Response.Purchase;
public class PurchaseDetailResponse
{
    public int PurchaseDetailId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public int QuantityReceived { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Subtotal { get; set; }
    public string? Observations { get; set; }
}