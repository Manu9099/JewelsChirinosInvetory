namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// Auditoría completa de movimientos
    /// Cada cambio en inventario crea un registro aquí
    /// </summary>
    public class InventoryMovement
    {
        public long MovementId { get; set; }
        public int ProductId { get; set; }
        
        public string MovementType { get; set; } = null!; // ENTRY, SALE, ADJUSTMENT, DAMAGED, RESERVE, RETURN
        public int Quantity { get; set; }
        public int? StockBefore { get; set; }
        public int? StockAfter { get; set; }
 
        public string? ReferenceType { get; set; } // PURCHASE, SALE, MANUAL
        public int? ReferenceId { get; set; } // ID de Compra/Venta
        
        public int? UserId { get; set; }
        public string? Observations { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}