namespace JewelShrinos.Application.DTOs.Response.Purchase;

public class PurchaseResponse
{
    public int PurchaseId { get; set; }
    public string PurchaseNumber { get; set; } = null!;
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string PurchaseStatus { get; set; } = null!;
    public string? Observations { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PurchaseDetailResponse> PurchaseDetails { get; set; } = new();
}

