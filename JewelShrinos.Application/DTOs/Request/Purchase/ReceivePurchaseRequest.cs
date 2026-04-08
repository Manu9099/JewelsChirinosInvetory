 namespace JewelShrinos.Application.DTOs.Request.Purchase;
 

 public class ReceivePurchaseRequest
{
    public List<ReceiveDetailRequest> Details { get; set; } = new();
    public string? Observations { get; set; }
}
 
public class ReceiveDetailRequest
{
    public int PurchaseDetailId { get; set; }
    public int QuantityReceived { get; set; }
}