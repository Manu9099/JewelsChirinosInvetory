namespace JewelShrinos.Application.DTOs.Request.Product;
 
public class CreateProductRequest
{
    public string Code { get; set; } = null!;
    public string? Barcode { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public int? MaterialId { get; set; }
    public int SupplierId { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal? Weight { get; set; }
    public string? Certificate { get; set; }
    public string? ImageUrl { get; set; }
    public string? Sku { get; set; }
}