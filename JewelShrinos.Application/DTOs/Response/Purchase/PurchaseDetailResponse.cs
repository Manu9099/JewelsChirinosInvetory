   public class PurchaseDetailResponse
    {
        public int PurchaseDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityPending => Quantity - QuantityReceived;
    }