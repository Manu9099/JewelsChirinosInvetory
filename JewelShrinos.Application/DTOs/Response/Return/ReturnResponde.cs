namespace JewelShrinos.Application.DTOs.Response.Return;
    public class ReturnResponse
    {
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!;
        public int SaleId { get; set; }
        public string? SaleNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string Reason { get; set; } = null!;
        public decimal? RefundAmount { get; set; }
        public string ReturnStatus { get; set; } = null!;
        public List<ReturnDetailResponse> ReturnDetails { get; set; } = new();
        public DateTime RequestDate { get; set; }
    }

    public class ReturnDetailResponse
    {
        public int ReturnDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int QuantityReturned { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
    }