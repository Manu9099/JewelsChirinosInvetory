          public class SaleDetailResponse
    {
        public int SaleDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal LineDiscount { get; set; }
        public string DetailStatus { get; set; } = null!;
    }
