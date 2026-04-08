using JewelShrinos.Core.Entities;
using System.Linq.Expressions;

/// <summary>
    /// Servicio de Inventario
    /// Control de stock
    /// </summary>
 
namespace JewelShrinos.Core.Interfaces
{ 

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
    public interface IInventoryService
    {
        Task<InventoryResponse?> GetInventoryAsync(int productId);
        Task<IEnumerable<InventoryResponse>> GetLowStockAsync(int threshold = 5);
        Task<int> AdjustStockAsync(int productId, int quantity, string movementType, int? userId = null, string? observations = null);
        Task<bool> ReserveAsync(int productId, int quantity, int? userId = null);
        Task<bool> ReleaseReserveAsync(int productId, int quantity, int? userId = null);
        Task<IEnumerable<InventoryMovementResponse>> GetMovementsAsync(int productId);
    }
}