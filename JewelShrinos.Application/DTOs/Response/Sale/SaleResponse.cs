namespace JewelShrinos.Application.DTOs.Response.Sale;
public class SaleResponse
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = null!;
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string SaleStatus { get; set; } = null!;
        public string? InvoiceNumber { get; set; }
        public string? SunatTicketNumber { get; set; }
        public List<SaleDetailResponse> SaleDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
