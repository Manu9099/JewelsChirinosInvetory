namespace JewelShrinos.Application.DTOs.Request.Supplier;

public class CreateSupplierRequest
{
    public string Name { get; set; } = null!;
    public string? RucDni { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}