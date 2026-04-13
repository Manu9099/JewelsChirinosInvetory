namespace JewelShrinos.Application.DTOs.Response.Supplier;

public class SupplierResponse
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = null!;
    public string? RucDni { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
}