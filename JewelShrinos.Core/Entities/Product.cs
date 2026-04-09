namespace JewelShrinos.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = null!; // Código único
        public string? Barcode { get; set; }
        public string? QrCode { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        
        public int CategoryId { get; set; }
        public int? MaterialId { get; set; }
        public int SupplierId { get; set; }
 
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal? Weight { get; set; } // En gramos
        public string? Certificate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Sku { get; set; }
 
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Category? Category { get; set; }
        public virtual Material? Material { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    }
}