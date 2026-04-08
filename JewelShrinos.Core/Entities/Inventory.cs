namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// Control de stock actual del producto
    /// 🔥 Aquí se descarga automáticamente al vender
    /// 🔥 Aquí se carga automáticamente al recibir compra
    /// </summary>
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        
        public int AvailableStock { get; set; } = 0; // Listo para vender
        public int ReservedStock { get; set; } = 0; // Reservado por clientes
        public int SoldStock { get; set; } = 0; // Vendido (informativo)
        public int DamagedStock { get; set; } = 0; // Dañado no recuperable
 
        public decimal? LastCostPrice { get; set; }
        public decimal? LastSellingPrice { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Propiedades computadas
        public int TotalStock => AvailableStock + ReservedStock;
        public int TotalExit => SoldStock + DamagedStock;
 
        // Relaciones
        public virtual Product? Product { get; set; }
    }
}