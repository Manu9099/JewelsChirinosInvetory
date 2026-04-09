namespace JewelShrinos.Core.Entities;

public class InventoryMovement
{
    public int InventoryMovementId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int InventoryId { get; set; }
    public Inventory Inventory { get; set; } = null!;

    public string MovementType { get; set; } = null!;
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}