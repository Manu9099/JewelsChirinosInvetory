// JewelShrinos.Application/DTOs/Response/Product/ProductResponse.cs
namespace JewelShrinos.Application.DTOs.Response.Product;

public class ProductResponse
{
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string? Barcode { get; set; }
    public string? QrCode { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? MaterialId { get; set; }
    public string? MaterialName { get; set; }
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal MarginPercentage { get; set; }
    public decimal? Weight { get; set; }
    public bool Status { get; set; }
}
 