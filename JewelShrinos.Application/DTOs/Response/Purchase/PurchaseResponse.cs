  public class PurchaseResponse
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string PurchaseStatus { get; set; } = null!;
        public List<PurchaseDetailResponse> PurchaseDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
   