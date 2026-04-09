namespace JewelShrinos.Application.DTOs.Request.Sale;

public class CreateSaleRequest
{
    public int? CustomerId { get; set; }
    public List<SaleDetailRequest> SaleDetails { get; set; } = new();

    public decimal TaxAmount { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;

    public string? PaymentMethod { get; set; }
    public string? Observations { get; set; }
    public string? CreatedBy { get; set; }
}
