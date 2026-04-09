namespace JewelShrinos.Application.DTOs.Request.Product;
public class UpdateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public int? MaterialId { get; set; }
    public int? SupplierId { get; set; }
    public decimal? CostPrice { get; set; }
    public decimal? SellingPrice { get; set; }
    public decimal? Weight { get; set; }
    public bool? Status { get; set; }
}