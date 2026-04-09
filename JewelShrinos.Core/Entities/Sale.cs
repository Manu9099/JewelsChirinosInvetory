namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// 🔥 VENTA - Cuando se crea:
    /// 1. Se inserta en Sales
    /// 2. Se insertan detalles
    /// 3. 🔥 INVENTARIO SE DESCARGA AUTOMÁTICAMENTE
    /// 4. 🔥 Se registra movimiento
    /// 5. Se genera factura SUNAT
    /// </summary>
    public class Sale
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = null!; // V-YYYYMMDD-NNN
        public int? CustomerId { get; set; }
        
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }
 
        public string? PaymentMethod { get; set; } // CASH, CARD, TRANSFER, MIXED, CHECK
        public string SaleStatus { get; set; } = "COMPLETED";
        // COMPLETED, CANCELLED, PARTIAL_RETURN, FULL_RETURN
        
        // SUNAT Integration
        public string? InvoiceNumber { get; set; } // Número de factura
        public string? InvoiceUrl { get; set; } // URL del PDF
        public string? SunatResponseCode { get; set; } // Código respuesta SUNAT
        public string? SunatTicketNumber { get; set; } // Ticket SUNAT
        
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
        // Relaciones
        public virtual Customer? Customer { get; set; }
        public Product Product { get; set; } = null!;
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
        public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
    }
}