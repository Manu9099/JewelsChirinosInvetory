
 
public class CreatePurchaseRequest
{
    public int SupplierId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<PurchaseDetailRequest> PurchaseDetails { get; set; } = new();
    public string? Observations { get; set; }
    public string? CreatedBy { get; set; }
}