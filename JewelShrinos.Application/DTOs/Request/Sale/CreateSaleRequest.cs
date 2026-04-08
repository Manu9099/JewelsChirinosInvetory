
public class CreateSaleRequest
{
    public int? CustomerId { get; set; }
    public List<SaleDetailRequest> SaleDetails { get; set; } = new();
    public decimal? DiscountAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Observations { get; set; }
    public string? CreatedBy { get; set; }

}

