namespace JewelShrinos.Application.DTOs.Request.Supplier;

public class UpdateSupplierRequest
{
    public string? Name { get; set; }
    public string? RucDni { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool? Status { get; set; }
}