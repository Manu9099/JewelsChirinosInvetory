  public class InventoryResponse
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int AvailableStock { get; set; }
        public int ReservedStock { get; set; }
        public int SoldStock { get; set; }
        public int DamagedStock { get; set; }
        public int TotalStock { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
    }
