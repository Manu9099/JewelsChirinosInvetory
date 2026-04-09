    namespace JewelShrinos.Application.DTOs.Response.Inventory;
     public class InventoryMovementResponse
    {
        public long MovementId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string MovementType { get; set; } = null!;
        public int Quantity { get; set; }
        public int? StockBefore { get; set; }
        public int? StockAfter { get; set; }
        public string? ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public string? Observations { get; set; }
        public DateTime MovementDate { get; set; }
    }
