  namespace JewelShrinos.Application.DTOs.Request.Purchase;
public class PurchaseDetailRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}