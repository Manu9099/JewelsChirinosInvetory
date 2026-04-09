namespace JewelShrinos.Core.Entities;

public class InventoryMovement
{
    public int InventoryMovementId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string MovementType { get; set; } = null!;
    public int Quantity { get; set; }

    public int? StockBefore { get; set; }
    public int? StockAfter { get; set; }

    public string? ReferenceType { get; set; }
    public int? ReferenceId { get; set; }
    public int? UserId { get; set; }

    public string? Observations { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;
}