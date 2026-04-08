namespace JewelShrinos.Application.DTOs.Request.Sale;
public class SaleDetailRequest
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? LineDiscount { get; set; }
}